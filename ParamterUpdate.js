function PrameterUpdate(updatedScript1) {
  
  const lines = updatedScript1.split('\n');
  const varNames = new Set();
  const varReplacements = new Map();

  // Step 1: Identify variables to replace
  for (const line of lines) {
    const match = line.match(/^{([a-zA-Z0-9]+)}$/);
    if (match) {
      const varName = match[1];
      if (!varName.startsWith('C_') && !varName.startsWith('c_') && !varName.startsWith('C') && !varName.startsWith('c') && !varName.startsWith('p') && !varName.startsWith('P') && !varName.startsWith('P_')) {
        varNames.add(varName);
      }
    }
  }

  // Step 2: Generate replacement map
  for (const varName of varNames) {
    const regex = new RegExp(`\\b${varName}\\b`, 'g');
    const replacement = `P${varName}`;
    varReplacements.set(regex, replacement);
  }

  // Step 3: Replace variables in script
  let updatedScript2 = '';
  for (const line of lines) {
    let updatedLine = line;
    for (const [regex, replacement] of varReplacements) {
      updatedLine = updatedLine.replace(regex, replacement);
    }
    updatedScript2 += updatedLine + '\n';
  }

  return updatedScript2;

}
