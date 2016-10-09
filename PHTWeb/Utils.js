
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};



function beep() {
    var snd = new Audio("data:audio/wav;base64,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qJrx4KOQjahZxWbcZgztj2c49nKmkId44S71j0c8eV9yDK6uPRzx5X18eDvjvQ6yKo9ZSS6l//8elePK/Lf//IInrOF/FvDoADYAGBMGb7FtErm5MXMlmPAJQVgWta7Zx2go+8xJ0UiCb8LHHdftWyLJE0QIAIsI+UbXu67dZMjmgDGCGl1H+vpF4NSDckSIkk7Vd+sxEhBQMRU8j/12UIRhzSaUdQ+rQU5kGeFxm+hb1oh6pWWmv3uvmReDl0UnvtapVaIzo1jZbf/pD6ElLqSX+rUmOQNpJFa/r+sa4e/pBlAABoAAAAA3CUgShLdGIxsY7AUABPRrgCABdDuQ5GC7DqPQCgbbJUAoRSUj+NIEig0YfyWUho1VBBBA//uQZB4ABZx5zfMakeAAAAmwAAAAF5F3P0w9GtAAACfAAAAAwLhMDmAYWMgVEG1U0FIGCBgXBXAtfMH10000EEEEEECUBYln03TTTdNBDZopopYvrTTdNa325mImNg3TTPV9q3pmY0xoO6bv3r00y+IDGid/9aaaZTGMuj9mpu9Mpio1dXrr5HERTZSmqU36A3CumzN/9Robv/Xx4v9ijkSRSNLQhAWumap82WRSBUqXStV/YcS+XVLnSS+WLDroqArFkMEsAS+eWmrUzrO0oEmE40RlMZ5+ODIkAyKAGUwZ3mVKmcamcJnMW26MRPgUw6j+LkhyHGVGYjSUUKNpuJUQoOIAyDvEyG8S5yfK6dhZc0Tx1KI/gviKL6qvvFs1+bWtaz58uUNnryq6kt5RzOCkPWlVqVX2a/EEBUdU1KrXLf40GoiiFXK///qpoiDXrOgqDR38JB0bw7SoL+ZB9o1RCkQjQ2CBYZKd/+VJxZRRZlqSkKiws0WFxUyCwsKiMy7hUVFhIaCrNQsKkTIsLivwKKigsj8XYlwt/WKi2N4d//uQRCSAAjURNIHpMZBGYiaQPSYyAAABLAAAAAAAACWAAAAApUF/Mg+0aohSIRobBAsMlO//Kk4soosy1JSFRYWaLC4qZBYWFRGZdwqKiwkNBVmoWFSJkWFxX4FFRQWR+LsS4W/rFRb/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////VEFHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAU291bmRib3kuZGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAwNGh0dHA6Ly93d3cuc291bmRib3kuZGUAAAAAAAAAACU=");
    snd.play();
}


function escapeHtml(str) {
    return str.replace(/[&<>"]/g, replaceHtmlEscapedChars);
}

function unEscapeHtml(str) {
    return str.replace(/(&amp;|&lt;|&gt;|&quot;)/g, replaceHtmlUnEscapedChars);
}

function escapeHtmlAttribute(str) {
    return str.replace(/[&<>"]/g, replaceHtmlEscapedChars);
}

function unEscapeHtmlAttribute(str) {
    return str.replace(/(&amp;|&lt;|&gt;|&quot;)/g, replaceHtmlUnEscapedChars);
}

var htmlEscapeReplacements = {
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;'
};

function replaceHtmlEscapedChars(char) {
    return htmlEscapeReplacements[char] || char;
}

var htmlUnEscapeReplacements = {
    '&amp;' : '&',
    '&lt;'  : '<',
    '&gt;'  : '>',
    '&quot;': '"'
};

function replaceHtmlUnEscapedChars(char) {
    return htmlUnEscapeReplacements[char] || char;
}




function escapeJSLiterial(str) {
    return str.replace(/['"\\]/g, replaceJSLiteralEscapeChars);
}

function unEscapeJSLiterial(str) {
    return str.replace(/(\\'|\\"|\\\\)/g, replaceJSLiteralUnEscapeChars);
}

var jsLiteralEscapeReplacements = {
    '\'': '\\\'',
    '\"': '\\\"',
    '\\': '\\\\'
};

function replaceJSLiteralEscapeChars(char) {
    return jsLiteralEscapeReplacements[char] || char;
}

var jsLiteralUnEscapeReplacements = {
    '\\\'' : '\'',
    '\\\"' : '\"',
    '\\\\' : '\\'
};

function replaceJSLiteralUnEscapeChars(char) {
    return jsLiteralUnEscapeReplacements[char] || char;
}

/**
 * Get the checked state of the input element specified by it's id.
 *
 * @param {string} id The id of the input element who's checked status will be returned
 * @param {boolean} [defaultValue=false] The default state to return if the element is not found.
 *
 * @return {boolean} The checked state of the element specified by the id, if the element does not exist the defaultValue is returned.
 */
function getIsChecked(id, defaultValue) {
    var elem = document.getElementById(id);

    if (elem) {
        return elem.checked;
    }

    if (typeof defaultValue !== 'undefined') {
        return defaultValue;
    }

    return false;
}

/**
 * Set the checked state of the input element specified by it's id.
 *
 * @param {string} id The id of the input element who's checked status will be returned
 * @param {boolean} value The value to set the element's checked state too.
 */
function setIsChecked(id, value) {
    var elem = document.getElementById(id);

    if (elem) {
        elem.checked = value;
    }
}

/**
 * Set the value of the input element with the specified id.
 *
 * @param {string} id The id of the input element who's value will be set.
 * @param {string} value The value that will be assigned to the input element if it exists.
 */
function setInputValue(id, value) {
    /** @type {HTMLElement} */
    var elem = document.getElementById(id);

    if (elem) {
        elem.value = value;
    }
}

/**
 * Get the value of the input element with the specified id.
 *
 * @param {string} id The id of the HTMLElement whos value will be returned.
 * @param {string} [defaultValue=""] The value to return if the element does not exist.
 *
 * @return {string} The value of the specified input field or the defaultValue if the element does not exist.
 */
function getInputValue(id, defaultValue) {
    /** @type {HTMLElement} */
    var elem = document.getElementById(id);

    if (elem) {
        return elem.value;
    }

    if (defaultValue) {
        return defaultValue;
    }

    return "";
}

/**
 * Get the numeric value contained in an input field, or return the default value if what is contained in the input
 * field is not a number.
 *
 * @param {string} id The id of the text input element that the number is contained in.
 * @param {number} [defaultValue=0] The default value to return if the element did not exist or did not contain a number.
 *
 * @return {number} The number representation of the contents of the input field specified by the id provided or the defaultValue
 *                  if the element does not exist or does not contain a number.
 */
function getNumericValue(id, defaultValue) {
    /** @type {HTMLElement} */
    var elem = document.getElementById(id);

    if (elem) {
        /** @type {number} */
        var result = parseInt(elem.value.trim());
        if (!isNaN(result)) {
            return result;
        }
    }
    
    if (defaultValue) {
        return defaultValue;
    }

    return 0;
}

/**
 * Takes a duration in milliseconds and returns a string in the form of {HH:}MM:SS:mmm
 *
 * @param {number} duration The duration in milliseconds to conver to a display string.
 *
 * @return {string} A string representing the milliseconds value in the format "{HH:}MM:SS:mmm".
 */
function getDurationText(duration) {
    /** @type {string} */
    var text = ""

    if (duration > 0) {
        var millis = (duration % 1000);
        duration -= millis;

        var seconds = (duration / 1000) % 60;
        duration -= (seconds * 1000);

        var minutes = (duration / (1000 * 60)) % 60;
        duration -= (minutes * 1000 * 60);

        var hours = duration / (1000 * 60 * 60);

        if (hours > 0) {
            text += hours;
            text += ":";
        }

        if (minutes < 10) text += "0";
        text += minutes;
        text += ":";

        if (seconds < 10) text += "0";
        text += seconds;
        text += ":";

        if (millis < 10) {
            text += "00";
        } else if (millis < 100) {
            text += "0";
        }
        text += millis;
    }

    return text;
}


function getDictionaries() {
    var dictionaries = "";

    var dictionaryIds = ["WordList", "ModernEnglish", "MiddleEnglish"];
    var dictionaryValues = ["0", "1", "2"];

    for (var i = 0; i < dictionaryIds.length; i++) {
        if (getIsChecked(dictionaryIds[i])) {
            dictionaries += dictionaryValues[i];
        }
    }

    return dictionaries
}

function getMinFrequency() {
    return getNumericValue("MinFrequency", 0);
}

function getMaxResults() {
    return getNumericValue("MaxResults", 999);
}

function setResultsCount(uniqueCount, fullCount) {
    if ((typeof fullCount !== 'undefined') && uniqueCount != fullCount) {
        setInputValue("ResultsCount", "" + uniqueCount + " (" + fullCount + ")");
    }

    setInputValue("ResultsCount", uniqueCount);
}

function setDuration(duration) {
    setInputValue("Duration", getDurationText(duration));
}

function doGetWordList() {
    PHTWords.Clear();
    PHTWords.AddText(getInputValue("WordListText"), getIsChecked("LinesAsWords"), getIsChecked("UseFrequency"));
}
