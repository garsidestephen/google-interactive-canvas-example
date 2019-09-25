using GoogleInteractiveCanvasExample.Models;
using GoogleInteractiveCanvasExample.Models.Payload;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GoogleInteractiveCanvasExample.Controllers
{
    /// <summary>
    /// Google Home Controller
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        private const string INTERACTIVECANVAS_URL = "https://your-domain/api/interactivecanvasdemo.html";

        /// <summary>
        /// Accepts Posts from Dialogflow
        /// </summary>
        /// <returns>Dialogflow Response</returns>
        public IHttpActionResult Post()
        {
            if (HttpContext.Current.Request.InputStream == null)
            {
                return BadRequest();
            }

            // Read Input stream from DF into a string
            string rawDialogflowRequestJson = null;

            using (var streamReader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                rawDialogflowRequestJson = streamReader.ReadToEnd();
            }

            if (!string.IsNullOrWhiteSpace(rawDialogflowRequestJson))
            {
                // Convert DF string into a dynamic
                var dialogflowRequest = JsonConvert.DeserializeObject<dynamic>(rawDialogflowRequestJson);

                // Grab Intent name and formulate a response
                if (dialogflowRequest?.queryResult?.intent?.displayName != null)
                {
                    string intentName = dialogflowRequest.queryResult.intent.displayName;

                    if (!string.IsNullOrWhiteSpace(intentName))
                    {
                        return Ok<DialogflowResponse>(CreateResponse(intentName, dialogflowRequest));
                    }
                }
            }

            // If all else fails return default fallback response
            return Ok<DialogflowResponse>(CreateFallbackResponse());
        }

        /// <summary>
        /// Creates a response to Dialogflow
        /// </summary>
        /// <param name="intentName">Intent Name fired</param>
        /// <param name="dialogflowRequest">The Dialogflow Request</param>
        /// <returns>A Dialogflow Response</returns>
        private static DialogflowResponse CreateResponse(string intentName, dynamic dialogflowRequest)
        {
            DialogflowResponse dialogflowResponse = null;

            if (!string.IsNullOrWhiteSpace(intentName))
            {
                switch (intentName.ToLower())
                {
                    case "welcome":
                        // Return a HTML response with the URL of your Interactive Canvas HTML Page
                        dialogflowResponse = CreateDialogflowHtmlResponse("ok, lets play! What colour box should I reveal, red, green or blue?", INTERACTIVECANVAS_URL);
                        break;
                    case "trigger_whatcolorbox":
                        // No colour specified, or colour already revealed so prompt user again
                        dialogflowResponse = CreateDialogflowHtmlResponse("What colour box should I reveal, red, green or blue?");
                        break;
                    case "trigger_correctchoice":
                        // User selected a colour and it had not been previously revealed so prompt for next choice
                        dialogflowResponse = CreateDialogflowHtmlResponse("Great choice! What colour box should I reveal next?");
                        break;
                    case "revealbox":
                        // Return a HTML response that triggers an action on the canvas + passes in an argument   
                        // Not passing a message to user from here as 'session / game state' handled by HTML page
                        var updatedState = new UpdatedState() { Command = "revealbox", Arguments = new string[] { dialogflowRequest.queryResult.parameters.Color } };
                        dialogflowResponse = CreateDialogflowHtmlResponse(null, null, updatedState);
                        break;
                    case "trigger_youwon":
                        // Trigger intent that has been triggered within the Interactive canvas
                        dialogflowResponse = CreateDialogflowHtmlResponse("you won the game, congratulations!", null, null, false);
                        break;
                }
            }

            if (dialogflowResponse == null)
            {
                // If none of the above return the fallback intent
                dialogflowResponse = CreateFallbackResponse();
            }

            return dialogflowResponse;
        }

        /// <summary>
        /// Create Fallback Response
        /// </summary>
        /// <returns>Dialogflow Fallback Response</returns>
        private static DialogflowResponse CreateFallbackResponse()
        {
            return CreateDialogflowHtmlResponse("Sorry, I dont understand what you have asked. Please ask me again.");
        }

        /// <summary>
        /// Create Dialogflow Basic Response
        /// </summary>
        /// <param name="expectUserResponse">If we expect a user response</param>
        /// <param name="textMessageToUser">The Text Message To User</param>
        /// <returns>Dialogflow Basic Response</returns>
        private static DialogflowResponse CreateDialogflowBasicResponse(bool expectUserResponse, string textMessageToUser = null)
        {
            var dialogflowResponse = new DialogflowResponse() { Payload = new PayloadWrapper() };

            dialogflowResponse.Payload.Google = new GooglePayload();
            dialogflowResponse.Payload.Google.ExpectUserResponse = expectUserResponse;

            if (!string.IsNullOrWhiteSpace(textMessageToUser))
            {
                var simpleResponseHolder = new SimpleResponseWrapper();
                simpleResponseHolder.SimpleResponse.TextToSpeech = textMessageToUser;

                dialogflowResponse.Payload.Google.RichResponse.Items.Add(simpleResponseHolder);
            }

            return dialogflowResponse;
        }

        /// <summary>
        /// Create Dialogflow Html Response
        /// </summary>
        /// <param name="textMessageToUser">The Text Message To User</param>
        /// <param name="interactiveCanvasUrl">The Interactive Canvas Url</param>
        /// <param name="updatedState">The Updated State</param>
        /// <param name="expectUserResponse">If we expect a user response</param>        
        /// <returns>Dialogflow Response</returns>
        private static DialogflowResponse CreateDialogflowHtmlResponse(string textMessageToUser = null, string interactiveCanvasUrl = null, UpdatedState updatedState = null, bool expectUserResponse = true)
        {
            DialogflowResponse dialogflowResponse = CreateDialogflowBasicResponse(expectUserResponse, textMessageToUser);

            // Regardless of whether there is anything in it, always return a HTML object in DF response
            // to keep the interactive canvas open
            var htmlResponseHolder = new HtmlResponseWrapper();

            if (!string.IsNullOrWhiteSpace(interactiveCanvasUrl) || updatedState != null)
            {
                // Do we have an interactive canavas url? (To kick off interaction)
                if (!string.IsNullOrWhiteSpace(interactiveCanvasUrl))
                {
                    // Tag end of URL with a GUID when in Dev to force IFrame reload
                    htmlResponseHolder.HtmlResponse.Url = $"{interactiveCanvasUrl}?t={Guid.NewGuid().ToString()}";
                }

                // Is there a state update?
                if (updatedState != null)
                {
                    htmlResponseHolder.HtmlResponse.UpdatedState = updatedState;
                }
            }

            dialogflowResponse.Payload.Google.RichResponse.Items.Add(htmlResponseHolder);

            return dialogflowResponse;
        }
    }
}
