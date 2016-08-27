///
/// PHTWords object definition.
///
var PHTWords = {};

PHTWords.Digits = "0123456789";
PHTWords.Punctuation = ";:.,!?[]{}()\"'“”‘’";
PHTWords.Whitespace = " \f\n\r\t\v";


PHTWords.TrimPunctuation = function (word, excludeText) {
    if (word) {
        if ((PHTWords.Punctuation.indexOf(word.charAt(word.length - 1)) > -1) && (!excludeText || excludeText.indexOf(word.charAt(word.length - 1)) > -1)) {
            return PHTWords.TrimPunctuation(word.substring(0, word.length - 1));
        }

        if ((PHTWords.Punctuation.indexOf(word.charAt(0)) > -1) && (!excludeText || excludeText.indexOf(word.charAt(0)) > -1)) {
            return PHTWords.TrimPunctuation(word.substring(1));
        }
    }

    return word;
}

PHTWords.IsWhitespace = function (char) {
    if (char) {
        return (char.match(/[ \f\n\r\t\v]/, char) != null);
    }

    return true;
}

PHTWords.EscapeString = function(text) {
    var characterToEscape = "|(){}$?!=+*.^[]%-_\\";  //SQL Server LIKE and JavaScript RegExp characters to escape.
    var escapeChar = '\\';

    var result = "";

    for (var i = 0; i < text.length; i++) {
        if ((text[i] == escapeChar[0]) || (characterToEscape.indexOf(text[i]) > -1)) {
            result = result + escapeChar;
        }
        result = result + text[i];
    }

    return result;
}

PHTWords.GetChangeLetterAnagramPatterns = function (word) {
    var patterns = [];

    if (word) {
        for (var i = 0; i < word.length; i++) {
            var wildCard = "[^" + word[i] + "]";
            var subWord = word.substring(0, i) + word.substring(i + 1);
            patterns = patterns.concat(PHTWords.GetAddLetterPatterns(subWord, wildCard));
        }
    }

    return patterns;
}

PHTWords.GetChangeLetterPatterns = function (word) {
    var patterns = [];

    if (word) {
        for (var i = 0; i < word.length; i++) {
            var start = i;
            var end = i;
            var wildCard = "[^" + PHTWords.EscapeString(word[i]) + "]"

            if ("_%".indexOf(word[i]) > -1) {
                wildCard = word[i];
            }
            else if (word[i] == "[") {
                for (i++; (i < word.length) && (word[i] != ']') ; i++) {
                }
                end = i;
                wildCard = "_";
            }

            patterns.push(word.substring(0, start) + wildCard + word.substring(end + 1));
        }
    }

    return patterns;
}

PHTWords.GetAddLetterPatterns = function (word, wildCard) {
    var patterns = [];

    if (word) {
        if (typeof (wildCard) === "undefined") {
            wildCard = "_";
        }

        for (var i = 0; i <= word.length; i++) {
            var start = i;
            var end = i;

            if (word[i] == '%') {
                continue;
            }
            else if (word[i] == "[") {
                for (i++; (i < word.length) && (word[i] != ']') ; i++) {
                }
                if (i < word.length) {
                    end = i + 1;
                }
                else {
                    end = i;
                }
            }

            patterns.push(word.substring(0, start) + wildCard + word.substring(end));
        }
    }

    return patterns;
}

PHTWords.GetRemoveLetterPatterns = function (word) {
    var patterns = [];

    if (word) {
        for (var i = 0; i < word.length; i++) {
            start = i;

            if (word[i] == '%') {
                continue;
            }
            else if (word[i] == "[") {
                for (i++; (i < word.length) && (word[i] != ']') ; i++) {
                }
            }

            end = i;

            patterns.push(word.substring(0, start) + word.substring(end + 1));
        }
    }

    return patterns;
}


PHTWords.Dictionary = []
PHTWords.WordIdxHash = {};
PHTWords.AnagramIdxHash = {};
PHTWords.CryptoIdxHash = {};
PHTWords.CryptoAnagramIdxHash = {};

PHTWords.Clear = function () {
    PHTWords.Dictionary = []
    PHTWords.WordIdxHash = {};
    PHTWords.AnagramIdxHash = {};
    PHTWords.CryptoIdxHash = {};
    PHTWords.CryptoAnagramIdxHash = {};
}

PHTWords.AddWord = function (word, frequency) {
    var entry = new DictionaryEntry(word, frequency);

    if (entry.word_idx && !PHTWords.WordIdxHash[entry.word_idx]) {
        PHTWords.Dictionary.push(entry);
        PHTWords.WordIdxHash[entry.word_idx] = entry;
        PHTWords.AddWordToHash(PHTWords.AnagramIdxHash, entry.anagram_idx, entry);
        PHTWords.AddWordToHash(PHTWords.CryptoIdxHash, entry.crypto_idx, entry);
        PHTWords.AddWordToHash(PHTWords.CryptoAnagramIdxHash, entry.crypto_anagram_idx, entry);
    }
}

PHTWords.AddWordToHash = function (hash, idx, entry) {
    if (hash[idx]) {
        hash[idx].push(entry);
    }
    else {
        hash[idx] = [entry];
    }
}

PHTWords.GetWordIdx = function (word) {
    if (word) {
        word = PHTWords.TrimPunctuation(word.trim()).replace(/[\s]+/g, "").toUpperCase();
    }

    return word;
};

PHTWords.GetAnagramIdx = function (word) {
    if (word) {
        word = word.replace(/[\s]+/g, "").toUpperCase();

        result = [];
        for (var i = 0; i < word.length; i++) {
            result.push(word.charAt(i));
        }

        result.sort(function (a, b) { if (a < b) { return -1; } else if (a > b) { return 1; } else { return 0; } });

        word = result.join("");
    }

    return word;
};

PHTWords.GetCryptoIdx = function (word) {
    if (word) {
        word = word.replace(/[\s]+/g, "").toUpperCase();

        var replacments = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        var result = "";
        var map = {};
        var map_count = 0;

        for (var i = 0; i < word.length; i++) {
            if (!map[word.charAt(i)]) {
                map[word.charAt(i)] = replacments.charAt(map_count);
                map_count++;
            }

            result += map[word.charAt(i)];
        }

        word = result;
    }

    return word;
};

PHTWords.GetCryptoAnagramIdx = function (word) {
    if (word) {
        word = word.replace(/[\s]+/g, "").toUpperCase();

        counts = {};
        for (var i = 0; i < word.length; i++) {
            if (counts[word.charAt(i)]) {
                counts[word.charAt(i)] = counts[word.charAt(i)] + 1;
            }
            else {
                counts[word.charAt(i)] = 1;
            }
        }

        var result = "";
        var keys = Object.keys(counts);
        var values = [];

        for (var i = 0; i < keys.length; i++) {
            values.push({ key: keys[i], value: counts[keys[i]] });
        }

        values.sort(function (a, b) { return a.value - b.value });

        for (var i = 0; i < values.length; i++) {
            for (var j = values[i].value; j > 0; j--) {
                result += values[i].key;
            }
        }

        word = PHTWords.GetCryptoIdx(result);
    }

    return word;
};


PHTWords.GetWordMatches = function (wordPatterns, anagramPatterns, cryptogramPatterns, cryptoAnagramPatterns, phoneticPatterns, dictionaries, minFrequency, maxResults, minWordLength, maxWordLength, callback) {
    var startTime = new Date().getTime();
    var asyncCall = false;

    if (!wordPatterns) {
        wordPatterns = "";
    }

    if (!anagramPatterns) {
        anagramPatterns = "";
    }

    if (!cryptogramPatterns) {
        cryptogramPatterns = "";
    }

    if (!cryptoAnagramPatterns) {
        cryptoAnagramPatterns = "";
    }

    if (!phoneticPatterns) {
        phoneticPatterns = "";
    }

    if (!dictionaries) {
        dictionaries = ""
    }

    if (!minFrequency) {
        minFrequency = 0;
    }

    if (!maxResults) {
        maxResults = 25;
    }

    if (!minWordLength) {
        minWordLength = 0;
    }

    if (!maxWordLength) {
        maxWordLength = 0;
    }

    if (callback) {
        asyncCall = true;
    }

    var results = PHTWords.GetWordMatchesLocal(wordPatterns, anagramPatterns, cryptogramPatterns, cryptoAnagramPatterns, dictionaries, minFrequency, maxResults, minWordLength, maxWordLength);

    if (dictionaries != "0") {
        if (results.length < maxResults) {
            $.ajax({
                url: '/api/GetWordMatches.aspx?dictionaries=' + encodeURIComponent(dictionaries) + "&minFrequency=" + encodeURIComponent(minFrequency) + "&maxResults=" + encodeURIComponent(maxResults) + "&minWordLength=" + encodeURIComponent(minWordLength) + "&maxWordLength=" + encodeURIComponent(maxWordLength)
                    + "&wordPatterns=" + encodeURIComponent(wordPatterns) + "&anagramPatterns=" + encodeURIComponent(anagramPatterns) + "&cryptogramPatterns=" + encodeURIComponent(cryptogramPatterns) + "&cryptoAnagramPatterns=" + encodeURIComponent(cryptoAnagramPatterns) + "&phoneticPatterns=" + encodeURIComponent(phoneticPatterns),
                type: 'GET',
                dataType: 'json',
                contentType: "text/json; charset=utf-8",
                success: function (data) {
                    var localCount = results.length;
                    for (var i = 0; i < data.length; i++) {
                        var j = 0;
                        for (; j < localCount; j++) {
                            if (results[j].value == data[i].value) {
                                break;
                            }
                        }
                        if (j >= localCount) {
                            results.push(data[i]);
                        }
                    }

                    if (callback) {
                        callback(results, startTime);
                    }
                },
                async: asyncCall,
            });
        }
    }
    else {
        if (callback) {
            callback(results, startTime);
        }
    }

    return results;
}

PHTWords.GetWordMatchesLocal = function (wordPatterns, anagramPatterns, cryptogramPatterns, cryptoAnagramPatterns, dictionaries, minFrequency, maxResults, minWordLength, maxWordLength) {
    var results = [];

    if ((dictionaries == "") || (dictionaries.indexOf("0") > -1)) {
        var wordMatchTests = [];
        var anagramMatchTests = [];
        var cryptogramMatchTests = [];
        var cryptoAnagramMatchTests = [];

        PHTWords.AppendPatterns(wordPatterns, wordMatchTests);
        PHTWords.AppendPatterns(anagramPatterns, anagramMatchTests);
        PHTWords.AppendPatterns(cryptogramPatterns, cryptogramMatchTests);
        PHTWords.AppendPatterns(cryptoAnagramPatterns, cryptoAnagramMatchTests);

        for (var i = 0; i < PHTWords.Dictionary.length && results.length < maxResults; i++) {
            if ((PHTWords.Dictionary[i].length >= minWordLength) && (!maxWordLength || (PHTWords.Dictionary[i].length <= maxWordLength)) && (PHTWords.Dictionary[i].frequency >= minFrequency)) {
                var include = true;

                for (var j = 0; j < wordMatchTests.length && include; j++) {
                    if (wordMatchTests[j].regex.test(PHTWords.Dictionary[i].word_idx) != wordMatchTests[j].result) {
                        include = false;
                    }
                }

                for (var j = 0; j < anagramMatchTests.length && include; j++) {
                    if (anagramMatchTests[j].regex.test(PHTWords.Dictionary[i].anagram_idx) != anagramMatchTests[j].result) {
                        include = false;
                    }
                }

                for (var j = 0; j < cryptogramMatchTests.length && include; j++) {
                    if (cryptogramMatchTests[j].regex.test(PHTWords.Dictionary[i].crypto_idx) != cryptogramMatchTests[j].result) {
                        include = false;
                    }
                }

                for (var j = 0; j < cryptoAnagramMatchTests.length && include; j++) {
                    if (cryptoAnagramMatchTests[j].regex.test(PHTWords.Dictionary[i].crypto_anagram_idx) != cryptoAnagramMatchTests[j].result) {
                        include = false;
                    }
                }

                if (include) {
                    results.push(new WordInfo(PHTWords.Dictionary[i].word_idx, PHTWords.Dictionary[i].frequency));
                }
            }
        }
    }

    return results;
}

PHTWords.AppendPatterns = function (patterns, patternMatches) {
    if (patterns) {
        patterns = patterns.toUpperCase().replace(/\s/g, "").replace(/_/g, ".").replace(/%/g, ".*"); // TODO: does not handle escaped of "%" characters properly.

        var andClauses = patterns.split('&');

        for (var i = 0; i < andClauses.length; i++) {
            if (andClauses[i].indexOf("!") == 0) {
                patternMatches.push({ "regex": new RegExp("^(" + andClauses[i].substring(1) + ")$"), "result": false });
            }
            else {
                patternMatches.push({ "regex": new RegExp("^(" + andClauses[i] + ")$"), "result": true });
            }
        }
    }
}





///
/// DictionaryEntry object definition
///
function DictionaryEntry(word, frequency) {
    this.frequency = frequency;
    this.word_idx = PHTWords.GetWordIdx(word);
    this.anagram_idx = PHTWords.GetAnagramIdx(this.word_idx);
    this.crypto_idx = PHTWords.GetCryptoIdx(this.word_idx);
    this.crypto_anagram_idx = PHTWords.GetCryptoAnagramIdx(this.word_idx);
    this.length = this.word_idx.length;
}


///
/// WordInfo object definition
///
function WordInfo(word, frequency) {
    this.value = word;
    this.frequency = frequency;
}

