var Braille = {
    LookupTable: [
        { value: "A", code: "200" },
        { value: "B", code: "220" },
        { value: "C", code: "300" },
        { value: "D", code: "310" },
        { value: "E", code: "210" },
        { value: "F", code: "320" },
        { value: "G", code: "330" },
        { value: "H", code: "230" },
        { value: "I", code: "120" },
        { value: "J", code: "130" },

        { value: "K", code: "202" },
        { value: "L", code: "222" },
        { value: "M", code: "302" },
        { value: "N", code: "312" },
        { value: "O", code: "212" },
        { value: "P", code: "322" },
        { value: "Q", code: "332" },
        { value: "R", code: "232" },
        { value: "S", code: "122" },
        { value: "T", code: "132" },

        { value: "ST", code: "102" },
        { value: "AR", code: "112" },

        { value: "U", code: "203" },
        { value: "V", code: "223" },
        { value: "X", code: "303" },
        { value: "Y", code: "313" },
        { value: "Z", code: "213" },
        { value: "AND", code: "323" },
        { value: "FOR", code: "333" },
        { value: "OF", code: "233" },
        { value: "THE", code: "123" },
        { value: "WITH", code: "133" },

        { value: "ING", code: "103" },
        { value: "BLE", code: "113" },  // -BLE and end of word otherwise it means number follows

        { value: "CH", code: "201" },
        { value: "GH", code: "221" },
        { value: "SH", code: "301" },
        { value: "TH", code: "311" },
        { value: "WH", code: "211" },
        { value: "ED", code: "321" },
        { value: "ER", code: "331" },
        { value: "OU", code: "231" },
        { value: "OW", code: "121" },
        { value: "W",  code: "131" },

        { value: "EA", code: "020" },
        { value: "BB", code: "022" },
        { value: "CC", code: "030" },
        { value: "DD", code: "031" },
        { value: "EN", code: "021" },
        { value: "FF", code: "032" },
        { value: "GG", code: "033" },
        { value: "?",  code: "023" },
        { value: "IN", code: "012" },
        { value: "BY", code: "013" },

        { value: "&", code: "011" },  // letter follows

        { value: " ", code: "000" },
        { value: "^", code: "001" },  // capital follows
        { value: "'", code: "002" },
        { value: "-", code: "003" },

    ],

    ToChar: function (code) {
        code = code.trim();
        for (var i = 0; i < Braille.LookupTable.length; i++) {
            if (Braille.LookupTable[i].code == code) {
                return Braille.LookupTable[i].value;
            }
        }

        return;
    },

    ToCode: function (value) {
        value = value.trim();
        for (var i = 0; i < Braille.LookupTable.length; i++) {
            if (Braille.LookupTable[i].value == value) {
                return Braille.LookupTable[i].code;
            }
        }

        return;
    },

    DrawFromCode: function(canvas, code) {
        var code = code.trim();

        var ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        var y = 3
        for (var row = 0; row < 3; row++) {
            var line = '0'

            if (row < code.length) {
                line = code[row];
            }

            switch (line) {
                case '1': {
                    ctx.beginPath();
                    ctx.arc(7, y, 1, 0, 2 * Math.PI);
                    ctx.fill();
                    ctx.beginPath();
                    ctx.arc(15, y, 3, 0, 2 * Math.PI);
                    ctx.fill();
                    break;
                }
                case '2': {
                    ctx.beginPath();
                    ctx.arc(7, y, 3, 0, 2 * Math.PI);
                    ctx.fill();
                    ctx.beginPath();
                    ctx.arc(15, y, 1, 0, 2 * Math.PI);
                    ctx.fill();
                    break;
                }
                case '3': {
                    ctx.beginPath();
                    ctx.arc(7, y, 3, 0, 2 * Math.PI);
                    ctx.fill();
                    ctx.beginPath();
                    ctx.arc(15, y, 3, 0, 2 * Math.PI);
                    ctx.fill();
                    break;
                }
                default: {
                    ctx.beginPath();
                    ctx.arc(7, y, 1, 0, 2 * Math.PI);
                    ctx.fill();
                    ctx.beginPath();
                    ctx.arc(15, y, 1, 0, 2 * Math.PI);
                    ctx.fill();
                    break;
                }
            }

            y += 7;
        }
    },

};