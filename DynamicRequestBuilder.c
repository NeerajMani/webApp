char *responseBodyOfFirstAPI; // Assuming response of 1st API is stored in a variable called responseBodyOfFirstAPI
char *requestJson;
char *sdlAccountActivityItemsJson;
char *tempJson;

int idCount;
int i;

web_custom_request("FirstAPI",
    "URL=first_api_url",
    "Method=GET",
    LAST);

responseBodyOfFirstAPI = lr_eval_string("{ResponseBody}");

// Parsing the JSON response and getting the number of IDs
web_convert_param("ResponseBody", "SourceEncoding=HTML", "TargetEncoding=PLAIN", LAST);
web_reg_save_param_json(
    "ParamName=ids",
    "QueryString=$.ids[*]",
    "ExtractAs=JSON",
    LAST);

web_convert_param("ids", "SourceEncoding=PLAIN", "TargetEncoding=HTML", LAST);
idCount = atoi(lr_eval_string("{ids_count}"));

//convert the time to readable format 
long unixTimestamp = 1676310337;
lr_save_datetime(unixTimestamp, "%Y-%m-%d %I:%M:%S %p", "convertedDateTime");

// Creating the SDLAccountActivityItems array
sdlAccountActivityItemsJson = "[";
for (i = 0; i < idCount; i++) {
    tempJson = "{"
        "\"Parent\": null,"
        "\"Id\": null,"
        "\"DateCreated\": \"2023-02-13 4:19 AM\","
        "\"ObligorActivityCodeId\": 3,"
        "\"AuthorAssociatedId\": 19143,"
        "\"AuthorName\": \"\","
        "\"Comments\": \"xxxx\","
        "\"CommentsRequired\": false,"
        "\"CommentsMissing\": false,"
        "\"NWD\": \"7/6/2020 12:00:00 AM\","
        "\"ObligarActivityCodeMissing\": false,"
        "\"OldDateCreated\": \"7/1/2020 12:53:00 PM\""
    "}";
    if (i > 0) {
        sdlAccountActivityItemsJson = lr_realloc(sdlAccountActivityItemsJson, strlen(sdlAccountActivityItemsJson) + strlen(",") + strlen(tempJson) + 1);
        strcat(sdlAccountActivityItemsJson, ",");
    }
    sdlAccountActivityItemsJson = lr_realloc(sdlAccountActivityItemsJson, strlen(sdlAccountActivityItemsJson) + strlen(tempJson) + 1);
    strcat(sdlAccountActivityItemsJson, tempJson);
}
sdlAccountActivityItemsJson = lr_realloc(sdlAccountActivityItemsJson, strlen(sdlAccountActivityItemsJson) + strlen("]") + 1);
strcat(sdlAccountActivityItemsJson, "]");

// Creating the JSON request for the 2nd API
requestJson = "{"
    "\"model\": {"
        "\"Id\": 8399,"
        "\"ObligorId\": \"0092000000002580124514\","
        "\"Bank\": \"0092\","
        "\"ObligorActivityResulotuionId\": null,"
        "\"ResolutionDate\": null,"
        "\"SDLAccountActivityItem\": " + lr_eval_string(sdlAccountActivityItemsJson) + ","
        "\"PAMAccountActivityItem\": [],"
        "\"DirectiveMemoro\": \"xxx\""
    "}"
"}";
// log the JSON request for debugging purposes
lr_log_message(requestJson); 

web_custom_request("SecondAPI",
    "URL=second_api_url",
    "Method=POST",
    "Body=" + requestJson,
    LAST);
