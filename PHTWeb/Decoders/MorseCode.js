var MorseCode = {

    // International Morse Code
    //

    LookupTable : [
        { value: "A", code: ".-" },
        { value: "B", code: "-..." },
        { value: "C", code: "-.-." },
        { value: "D", code: "-.." },
        { value: "E", code: "." },
        { value: "F", code: "..-." },
        { value: "G", code: "--." },
        { value: "H", code: "...." },
        { value: "I", code: ".." },
        { value: "J", code: ".---" },
        { value: "K", code: "-.-" },
        { value: "L", code: ".-.." },
        { value: "M", code: "--" },
        { value: "N", code: "-." },
        { value: "O", code: "---" },
        { value: "P", code: ".--." },
        { value: "Q", code: "--.-" },
        { value: "R", code: ".-." },
        { value: "S", code: "..." },
        { value: "T", code: "-" },
        { value: "U", code: "..-" },
        { value: "V", code: "...-" },
        { value: "W", code: ".--" },
        { value: "X", code: "-..-" },
        { value: "Y", code: "-.--" },
        { value: "Z", code: "--.." },

        { value: "0", code: "-----" },
        { value: "1", code: ".----" },
        { value: "2", code: "..---" },
        { value: "3", code: "...--" },
        { value: "4", code: "....-" },
        { value: "5", code: "....." },
        { value: "6", code: "-...." },
        { value: "7", code: "--..." },
        { value: "8", code: "---.." },
        { value: "9", code: "----." },

        { value: ".", code: ".-.-.-" },
        { value: ",", code: "--..--" },
        { value: ":", code: "---..." },
        { value: "-", code: "-....-" },
        { value: "'", code: ".----." },
        { value: "/", code: "-..-." },
        { value: "\"", code: "-.--.-" }, // Transmitted before and after word affected. This is normally parenthese or brackets, but rendering a quote so it is semetrical.
        { value: "_", code: "..--.-" },  // Transmitted before and after word affected.
        { value: "=", code: "-...-" },
        { value: "?", code: "..--.." },
        { value: "@", code: ".--.-." },

        { value: "Ä", code: ".-.-" },
        { value: "Å", code: ".--.-" },
        { value: "CH", code: "----" },
        { value: "É", code: "..-.." },
        { value: "Ñ", code: "--.--" },
        { value: "Ö", code: "---." },
        { value: "Ü", code: "..--" },
    ],

    ToChar: function (code) {
        code = code.trim();
        for (var i = 0; i < MorseCode.LookupTable.length; i++) {
            if (MorseCode.LookupTable[i].code == code) {
                return MorseCode.LookupTable[i].value;
            }
        }

        return;
    },

    ToCode: function (value) {
        value = value.trim();
        for (var i = 0; i < MorseCode.LookupTable.length; i++) {
            if (MorseCode.LookupTable[i].value == value) {
                return MorseCode.LookupTable[i].code;
            }
        }

        return;
    }

};