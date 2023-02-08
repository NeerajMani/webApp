function webRegFindUpdater(inputFile) {
  const lines = inputFile.split('\n');

  let transactionName = null;

  const updatedLines = lines.map((line) => {
    if (line.includes("lr_start_transaction")) {
      const start = line.indexOf('"') + 1;
      const end = line.lastIndexOf('"');
      transactionName = line.substring(start, end);

      while (!line.includes("lr_end_transaction")) {
        line = lines[++i];
        if (line.includes("web_custom_request") || line.includes("web_url")
                || line.includes("web_submit_data") || line.includes("web_submit_form")) {
          if (!lines[i - 1].includes("web_reg_find")) {
            line = `web_reg_find("Text=200","ID :{VuserID} | Iteration : {IterationId} | UserName : {PUsername} | TimeStamp : {TS} | Error at Transaction : ${transactionName}");\n${line}`;
          }
        }
      }
    }

    return line;
  });

  return updatedLines.
