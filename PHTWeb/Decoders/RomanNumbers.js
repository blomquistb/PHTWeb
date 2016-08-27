var RomanNumbers = {

    ToString: function (value) {
        result = "";

        for (var i = 0; i < RomanNumbers.BaseValues.length; i++) {
            while (value >= RomanNumbers.BaseValues[i].value) {
                result += RomanNumbers.BaseValues[i].digits;
                value -= RomanNumbers.BaseValues[i].value;
            }
        }

        return result;
    },

    Parse : function (s) {
        s = s.trim().toUpperCase();

        
        var result = 0;
        var pos = 0;

        // Use lookup table to parse string
        while (pos < s.length)
        {
            // Test for double-character match
            if ((s.length - pos) >= 2)
            {
                var pair = s.substring(pos, pos+2);

                var info = RomanNumbers.FindDigitsInfo(pair);
                if (info != null)
                {
                    result += info.value;
                    pos += 2;
                    continue;
                }
            }

            // Otherwise, text for single-character match
            var letter = s.substring(pos, pos+1);
            var info = RomanNumbers.FindDigitsInfo(letter);
            if (info != null)
            {
                result += info.value;
                pos++;
            }
            else
            {
                return Number.NaN;
            }
        }

        return result;
    },

    BaseValues: [
        { value: 1000, digits: "M" },
        { value: 900, digits: "CM" },
        { value: 500, digits: "D" },
        { value: 400, digits: "CD" },
        { value: 100, digits: "C" },
        { value: 90, digits: "XC" },
        { value: 50, digits: "L" },
        { value: 40, digits: "XL" },
        { value: 10, digits: "X" },
        { value: 9, digits: "IX" },
        { value: 5, digits: "V" },
        { value: 4, digits: "IV" },
        { value: 1, digits: "I" },
    ],

    FindDigitsInfo: function (s) {

        for (var i = 0 ; i < RomanNumbers.BaseValues.length; i++) {
            if (s == RomanNumbers.BaseValues[i].digits) {
                return RomanNumbers.BaseValues[i];
            }
        }

        return null;
    },

};