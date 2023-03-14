function updateAndReplaceScript(script) {
  let lines = script.split('\n');
  const varNames = [];
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];
    if (line.includes('web_reg_save_param(')) {
      const startIndex = line.indexOf('"') + 1;
      const endIndex = line.indexOf('"', startIndex + 1);
      const varName = line.substring(startIndex, endIndex);
      varNames.push(varName);
    } else if (line.includes('web_reg_save_param_ex(')) {
      const varName = line.split('ParamName=')[1].split('"')[0];
      varNames.push(varName);
    }
  }
  let updatedScript = '';
  for (let i = 0; i < lines.length; i++) {
    let line = lines[i];
    for (let varName of varNames) {
      const regex = new RegExp(varName, 'g');
      line = line.replace(regex, `C_${varName}`);
    }
    updatedScript += line + '\n';
  }

  const varReplacements = new Map();

  // Step 1: Identify variables to replace
  for (const line of updatedScript.split('\n')) {
    const match = line.match(/^{([a-zA-Z0-9]+)}$/);
    if (match) {
      const varName = match[1];
      if (!varName.startsWith('C_') && !varName.startsWith('c_') && !varName.startsWith('C') && !varName.startsWith('c') && !varName.startsWith('p') && !varName.startsWith('P') && !varName.startsWith('P_')) {
        varReplacements.set(varName, `P_${varName}`);
      }
    }
  }

  // Step 2: Replace variables in script
  let updatedScript2 = '';
  for (const line of updatedScript.split('\n')) {
    let updatedLine = line;
    for (const [varName, replacement] of varReplacements) {
      const regex = new RegExp(`\\b${varName}\\b`, 'g');
      updatedLine = updatedLine.replace(regex, replacement);
    }
    updatedScript2 += updatedLine + '\n';
  }

  return updatedScript2;
}
