// JavaScript source code
var assignments = {
    "L" : "8",
}
var expressions = [
"map.H*power[map.base][4]+map.O*power[base][3]+map.N*power[base][2]+map.O*power[base][1]+map.R + map.L*power[base][3]+map.I*power[base][2]+map.O*power[base][1]+map.N == map.T*power[base][4]+map.H*power[base][3]+map.R*power[base][2]+map.E*power[base][1]+map.E",
];

var map = {};
var dynTokens = ["E", "H", "I", "N", "O", "R", "T"];
var calTokens = ["L"];

var bases = [10];
var power = [10] = [1, 10, 100, 1000, 10000];

for(var base in bases) {

}


/**
 *
 */
function getMapping(text, ignoreCase, wordsAreTokens) {
    var result = {};
    var tokens = [];
    result.expressions = [];

    if (ignoreCase) {
        text = text.toUpperCase();
    }

    text = text.replace(/(\t| |\r|\n)+/g, '');          // remove tabs, spaces, line feed, and carriage return characters
    text = text.replace(/([^!=><])=([^=])/g, "$1==$2"); // replace assignment operator with equality comparison operator

    // find all the tokens and create the initial mapping object.
    tokens = getTokens(text, wordsAreTokens);

    for(var i=0; i < tokens.length; i++) {
        result.push({token: tokens[i], value: "", dependencies: []});
    }

    //
    var rawExpressions = text.split(';');
    for(var i = 0; i < rawExpressions.length; i++) {
        expression = rawExpressions[i];

        if (expression.indexOf('?') < 0) {
            var index = expression.indexOf('==');
            if (index >= 0) {
                var token;
                var assignment;
                var left = expression.substring(0, index);
                var right = expression.substring(index+2);

                if (tokens.indexOf(left) > -1) {
                    var token = left;
                    var assignment = right;
                }
                else if (tokens.indexOf(right) > -1) {
                    var token = right;
                    var assignment = left;
                }

                if (token) {
                    result[token].value = assignment;
                    result[token].dependencies = getTokens(assignment, wordsAreTokens);
                }
                else {
                    result.expressions.push(expression);
                }
            }
            else {
                result.expressions.push(expression);
            }
        }
        else {
            result.expressions.push(expression);
        }
    }

    //
    for (var i = 0; i < result.length; i++) {
        removeCircularDependencies(result[i], {});
    }

    return result;
}

/**
 *
 */
function getTokens(text, wordsAreTokens) {
    var tokens = [];

    if (wordsAreTokens) {   // every grouping of letters is a token
        var regex = /([A-Z]|[a-z])+/g;
        var tList = text.match(regex);
        for(var i=0; i < tList.length; i++) {
            if (tokens.indexOf(tList[i]) < 0) {
                tokens.push(tList[i]);
            }
        }
    }
    else {                  // every letter is a token
        for (var i = 0; i < text.length; i++) {
            var c = text.charAt(i);
            if (((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) && (tokens.indexOf(c) < 0)) {
                tokens.push(c);
            }
        }
    }

    return tokens;
}

/**
 *
 */
function removeCircularDependencies(mValue, mapping) {
    if (!mapping[mValue.token]) {
        if (mValue.value) {
            mapping[mValue.token] = 1;  // trying to define
            var dependencies = mValue.value.getTokens(mValue.value);
            for (var j = 0; j < dependencies.length; j++) {
                if (mapping[dependencies[j]] == 2) {
                    // do nothing and check next dependency.
                }
                else if (mapping[dependencies[j]] == 1) {   // if dependency is in trying state then this is a recursive dependency which needs to be removed.
                    mValue.value = "";
                    mValue.expression = null;
                    break;
                }
                else {
                    for (var i = 0; i < gMapping.length; i++) {
                        if (gMapping[i].digit == dependencies[j]) {
                            removeCircularDependencies(gMapping[i], mapping);
                        }
                    }
                }
            }
            mapping[mValue.digit] = 2;
        }
        else {
            mapping[mValue.digit] = 2; // fully resolved
        }
    }
}



function getExpressions(text, wordsAreValue) {
    if (getIgnoreCase()) {
        text = text.toUpperCase();
    }

    text = text.replace(/(\t| |\r|\n)+/g, '');          // remove tabs, spaces, line feed, and carriage return characters
    text = text.replace(/([^!=><])=([^=])/g, "$1==$2"); // replace assignment operator with equality comparison operator

    var tokens = getTokens(text);                       // get list of all tokens
    var txtExpressions = text.split(";");               // get each seperate expression

}

function getAssignments(expressions) {
    results = [];

    for(var i = 0; i < expressions.length; i++) {
        expression = expressions[i];

        if (expression.indexOf('?') < 0) {
            var index = expression.indexOf('==');
            if (index >= 0) {
                var token = expression.substring(0, index).trim();
            }
        }
        else (expression.indexOf('?') > -1) {
            var a = expression.split('?')[1];
            if (a.indexOf(':') >= 0) {

            }
        }
    }

    return results;
}

function transformExpression(expression, wordsAreValues) {
    var result = expression;

    if (wordsAreValues) {
        var regex = /([A-Z]|[a-z])+/g;
        var result = expression.replace(regex, function(t) {return "map." + t;});
    }
    else {
        var regex = /([A-Z]|[a-z])+/g;
        var result = expression.replace(regex, function(t) {
            var r = "(";
            for(var i = 0; i < t.length; i++) {
                var power = t.length - i - 1;
                r = r + "map." + t.charAt(i);
                if (power != 0) {
                    r = r + "*power[map.$][" + power + "]+";
                }
            }
            r = r + ")";
            return r;
        });
    }

    return result;
}


R*getBase(0)+O*pow(base, 1)+
if ()
if (!(L == 8))

function doSolve() {
    setInputValue("ResultsCount", "");
    setInputValue("Duration", "");
    renderResults([]);      // Clear the results.

    var text = getOriginalText();

    if (getIgnoreCase()) {
        text = text.toUpperCase();
    }


    text = text.replace(/(\t| |\r|\n)+/g, '');          // remove tabs, spaces, line feed, and carriage return characters
    text = text.replace(/([^!=><])=([^=])/g, "$1==$2"); // replace assignment operator with equality comparison operator
    var tokens = getTokens(text);                       // get list of all tokens
    var txtExpressions = text.split(";");               // get each seperate expression


    doMapChange();

    gExpressions = [];
    gLeadingDigits = "";

    for (var i = 0; i < txtExpressions.length; i++) {
        if (txtExpressions[i].trim() != "") {
            gExpressions.push(new CryptoExpression(txtExpressions[i]));
        }
    }

    for (var i = 0; i < gMapping.length; i++) {
        var digit = gMapping[i].digit;
        for (var j = 0; j < gExpressions.length; j++) {
            if (gExpressions[j].operator == '=') {
                if (gExpressions[j].left.digits == digit) {
                    if (gExpressions[j].right.value) {
                        gMapping[i].value = "" + gExpressions[j].right.value;
                        break;
                    } else {
                        gMapping[i].value = gExpressions[j].right.text;
                    }
                }

                if (gExpressions[j].right.digits == digit) {
                    if (gExpressions[j].left.value) {
                        gMapping[i].value = "" + gExpressions[j].left.value;
                        break;
                    } else {
                        gMapping[i].value = gExpressions[j].left.text;
                    }
                }
            }
        }
    }

    // Create expressions for all mapping values that have one.
    //
    for (var i = 0; i < gMapping.length; i++) {
        var v = gMapping[i].value.trim();
        if (v) {
            var value = +v;
            if (isNaN(value)) {
                // HACK: replace all occurances of '-' with '+0-' to fix precedence problem with evaulation engine.
                //v = v.replace(/\-/g, "+0-");
                // HACK: replace all occurances of '/' with '*1/' to fix precedence problem with evaulation engine.
                //v = v.replace(/\//g, "*1/");
                gMapping[i].expression = new CryptoExpression(v);
            }
        }
        else {
            gMapping[i].expression = null;
        }
    }

    for (var i = 0; i < gMapping.length; i++) {
        removeCircularDependencies(gMapping[i], {});
    }

    renderMappingTable(gMapping);


    window.setTimeout("doSolveWork();", 0);
}

