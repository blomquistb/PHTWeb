var Semaphore = {

    LookupTable: [
        ['?', '#', 'J', 'V', 'D', 'K', 'P', 'T'],
        ['#', '?', 'W', 'X', 'E', 'L', 'Q', 'U'],
        ['J', 'W', '?', 'Z', 'F', 'M', 'R', 'Y'],
        ['V', 'X', 'Z', '?', 'G', 'N', 'S', '*'],
        ['D', 'E', 'F', 'G', '.', 'A', 'B', 'C'],
        ['K', 'L', 'M', 'N', 'A', '?', 'H', 'I'],
        ['P', 'Q', 'R', 'S', 'B', 'H', '?', 'O'],
        ['T', 'U', 'V', '*', 'C', 'I', 'O', '?'],
    ],

    ToCharHHMM: function (value) {
        var values = value.trim().replace(/\./g, ':').split(':');

        if (values.length == 2) {
            var hour = parseInt(values[0].trim(), 10);
            if (isNaN(hour)) {
                return;
            }

            var minutes = parseInt(values[1].trim(), 10);
            if (isNaN(minutes)) {
                return;
            }

            return Semaphore.ToChar(Semaphore.HourToPos(hour), Semaphore.MinutesToPos(minutes));
        }

        return;
    },

    ToCharMMSS: function (value) {
        var values = value.trim().replace(/\./g, ':').split(':');

        if (values.length == 2) {
            var minutes = parseInt(values[0].trim(), 10);
            if (isNaN(minutes)) {
                return;
            }

            var seconds = parseInt(values[1].trim(), 10);
            if (isNaN(seconds)) {
                return;
            }

            return Semaphore.ToChar(Semaphore.MinutesToPos(minutes), Semaphore.SecondsToPos(seconds));
        }

        return;
    },

    ToChar: function (pos1, pos2) {
        return Semaphore.LookupTable[pos1][pos2];
    },

    HourToPos: function (hour) {
        hour = hour % 12;

        switch (hour) {
            case 0: { return 0; }
            case 1: { return 1; }
            case 2: { return 1; }
            case 3: { return 2; }
            case 4: { return 3; }
            case 5: { return 3; }
            case 6: { return 4; }
            case 7: { return 5; }
            case 8: { return 5; }
            case 9: { return 6; }
            case 10: { return 7; }
            case 11: { return 7; }
        }
    },

    MinutesToPos: function (minutes) {
        minutes = minutes % 60;

        if ((minutes <= 4) || (minutes >= 57)) {
            return 0;
        }
        else if ((minutes >= 5) && (minutes <= 11)) {
            return 1;
        }
        else if ((minutes >= 12) && (minutes <= 18)) {
            return 2;
        }
        else if ((minutes >= 19) && (minutes <= 25)) {
            return 3;
        }
        else if ((minutes >= 26) && (minutes <= 34)) {
            return 4;
        }
        else if ((minutes >= 35) && (minutes <= 40)) {
            return 5;
        }
        else if ((minutes >= 41) && (minutes <= 49)) {
            return 6;
        }
        else if ((minutes >= 50) && (minutes <= 56)) {
            return 7;
        }
    },

    SecondsToPos: function (seconds) {
        return Semaphore.MinutesToPos(seconds);
    },

};