var gPeriodicTable = [
    { "Symbol": "H", "AtomicNumber": 1, "AtomicWeight": 1.008, "Name": "Hydrogen" },
    { "Symbol": "He", "AtomicNumber": 2, "AtomicWeight": 4.003, "Name": "Helium" },
    { "Symbol": "Li", "AtomicNumber": 3, "AtomicWeight": 6.94, "Name": "Lithium" },
    { "Symbol": "Be", "AtomicNumber": 4, "AtomicWeight": 9.012, "Name": "Beryllium" },
    { "Symbol": "B", "AtomicNumber": 5, "AtomicWeight": 10.81, "Name": "Boron" },
    { "Symbol": "C", "AtomicNumber": 6, "AtomicWeight": 12.01, "Name": "Carbon" },
    { "Symbol": "N", "AtomicNumber": 7, "AtomicWeight": 14.01, "Name": "Nitrogen" },
    { "Symbol": "O", "AtomicNumber": 8, "AtomicWeight": 16.00, "Name": "Oxygen" },
    { "Symbol": "F", "AtomicNumber": 9, "AtomicWeight": 19.00, "Name": "Fluorine" },
    { "Symbol": "Ne", "AtomicNumber": 10, "AtomicWeight": 20.18, "Name": "Neon" },
    { "Symbol": "Na", "AtomicNumber": 11, "AtomicWeight": 22.99, "Name": "Sodium" },
    { "Symbol": "Mg", "AtomicNumber": 12, "AtomicWeight": 24.31, "Name": "Magnesium" },
    { "Symbol": "Al", "AtomicNumber": 13, "AtomicWeight": 26.98, "Name": "Aluminium" },
    { "Symbol": "Si", "AtomicNumber": 14, "AtomicWeight": 28.09, "Name": "Silicon" },
    { "Symbol": "P", "AtomicNumber": 15, "AtomicWeight": 30.97, "Name": "Phosphorus" },
    { "Symbol": "S", "AtomicNumber": 16, "AtomicWeight": 32.06, "Name": "Sulfur" },
    { "Symbol": "Cl", "AtomicNumber": 17, "AtomicWeight": 35.45, "Name": "Chlorine" },
    { "Symbol": "Ar", "AtomicNumber": 18, "AtomicWeight": 39.95, "Name": "Argon" },
    { "Symbol": "K", "AtomicNumber": 19, "AtomicWeight": 39.10, "Name": "Potassium" },
    { "Symbol": "Ca", "AtomicNumber": 20, "AtomicWeight": 40.08, "Name": "Calcium" },
    { "Symbol": "Sc", "AtomicNumber": 21, "AtomicWeight": 44.96, "Name": "Scandium" },
    { "Symbol": "Ti", "AtomicNumber": 22, "AtomicWeight": 47.88, "Name": "Titanium" },
    { "Symbol": "V", "AtomicNumber": 23, "AtomicWeight": 50.94, "Name": "Vanadium" },
    { "Symbol": "Cr", "AtomicNumber": 24, "AtomicWeight": 52.00, "Name": "Chromium" },
    { "Symbol": "Mn", "AtomicNumber": 25, "AtomicWeight": 54.94, "Name": "Manganese" },
    { "Symbol": "Fe", "AtomicNumber": 26, "AtomicWeight": 55.85, "Name": "Iron" },
    { "Symbol": "Co", "AtomicNumber": 27, "AtomicWeight": 58.93, "Name": "Cobalt" },
    { "Symbol": "Ni", "AtomicNumber": 28, "AtomicWeight": 58.69, "Name": "Nickel" },
    { "Symbol": "Cu", "AtomicNumber": 29, "AtomicWeight": 63.55, "Name": "Copper" },
    { "Symbol": "Zn", "AtomicNumber": 30, "AtomicWeight": 65.39, "Name": "Zinc" },
    { "Symbol": "Ga", "AtomicNumber": 31, "AtomicWeight": 69.72, "Name": "Gallium" },
    { "Symbol": "Ge", "AtomicNumber": 32, "AtomicWeight": 72.64, "Name": "Germanium" },
    { "Symbol": "As", "AtomicNumber": 33, "AtomicWeight": 74.92, "Name": "Arsenic" },
    { "Symbol": "Se", "AtomicNumber": 34, "AtomicWeight": 78.96, "Name": "Selenium" },
    { "Symbol": "Br", "AtomicNumber": 35, "AtomicWeight": 79.90, "Name": "Bromine" },
    { "Symbol": "Kr", "AtomicNumber": 36, "AtomicWeight": 83.79, "Name": "Krypton" },
    { "Symbol": "Rb", "AtomicNumber": 37, "AtomicWeight": 85.47, "Name": "Rubidium" },
    { "Symbol": "Sr", "AtomicNumber": 38, "AtomicWeight": 87.62, "Name": "Strontium" },
    { "Symbol": "Y", "AtomicNumber": 39, "AtomicWeight": 88.91, "Name": "Yttrium" },
    { "Symbol": "Zr", "AtomicNumber": 40, "AtomicWeight": 91.22, "Name": "Zirconium" },
    { "Symbol": "Nb", "AtomicNumber": 41, "AtomicWeight": 92.91, "Name": "Niobium" },
    { "Symbol": "Mo", "AtomicNumber": 42, "AtomicWeight": 95.96, "Name": "Molybdenum" },
    { "Symbol": "Tc", "AtomicNumber": 43, "AtomicWeight": 98.00, "Name": "Technetium" },
    { "Symbol": "Ru", "AtomicNumber": 44, "AtomicWeight": 101.1, "Name": "Ruthenium" },
    { "Symbol": "Rh", "AtomicNumber": 45, "AtomicWeight": 102.9, "Name": "Rhodium" },
    { "Symbol": "Pd", "AtomicNumber": 46, "AtomicWeight": 106.4, "Name": "Palladium" },
    { "Symbol": "Ag", "AtomicNumber": 47, "AtomicWeight": 107.9, "Name": "Silver" },
    { "Symbol": "Cd", "AtomicNumber": 48, "AtomicWeight": 112.4, "Name": "Cadmium" },
    { "Symbol": "In", "AtomicNumber": 49, "AtomicWeight": 114.8, "Name": "Indium" },
    { "Symbol": "Sn", "AtomicNumber": 50, "AtomicWeight": 118.7, "Name": "Tin" },
    { "Symbol": "Sb", "AtomicNumber": 51, "AtomicWeight": 121.8, "Name": "Antimony" },
    { "Symbol": "Te", "AtomicNumber": 52, "AtomicWeight": 127.6, "Name": "Tellurium" },
    { "Symbol": "I", "AtomicNumber": 53, "AtomicWeight": 126.9, "Name": "Iodine" },
    { "Symbol": "Xe", "AtomicNumber": 54, "AtomicWeight": 131.3, "Name": "Xenon" },
    { "Symbol": "Cs", "AtomicNumber": 55, "AtomicWeight": 132.9, "Name": "Caesium" },
    { "Symbol": "Ba", "AtomicNumber": 56, "AtomicWeight": 137.3, "Name": "Barium" },
    { "Symbol": "La", "AtomicNumber": 57, "AtomicWeight": 138.9, "Name": "Lanthanum" },
    { "Symbol": "Ce", "AtomicNumber": 58, "AtomicWeight": 140.1, "Name": "Cerium" },
    { "Symbol": "Pr", "AtomicNumber": 59, "AtomicWeight": 140.9, "Name": "Praseodymium" },
    { "Symbol": "Nd", "AtomicNumber": 60, "AtomicWeight": 144.2, "Name": "Neodymium" },
    { "Symbol": "Pm", "AtomicNumber": 61, "AtomicWeight": 145, "Name": "Promethium" },
    { "Symbol": "Sm", "AtomicNumber": 62, "AtomicWeight": 150.4, "Name": "Samarium" },
    { "Symbol": "Eu", "AtomicNumber": 63, "AtomicWeight": 152.0, "Name": "Europium" },
    { "Symbol": "Gd", "AtomicNumber": 64, "AtomicWeight": 157.2, "Name": "Gadolinium" },
    { "Symbol": "Tb", "AtomicNumber": 65, "AtomicWeight": 158.9, "Name": "Terbium" },
    { "Symbol": "Dy", "AtomicNumber": 66, "AtomicWeight": 162.5, "Name": "Dysprosium" },
    { "Symbol": "Ho", "AtomicNumber": 67, "AtomicWeight": 164.9, "Name": "Holmium" },
    { "Symbol": "Er", "AtomicNumber": 68, "AtomicWeight": 167.3, "Name": "Erbium" },
    { "Symbol": "Tm", "AtomicNumber": 69, "AtomicWeight": 168.9, "Name": "Thulium" },
    { "Symbol": "Yb", "AtomicNumber": 70, "AtomicWeight": 173.0, "Name": "Ytterbium" },
    { "Symbol": "Lu", "AtomicNumber": 71, "AtomicWeight": 175.0, "Name": "Lutetium" },
    { "Symbol": "Hf", "AtomicNumber": 72, "AtomicWeight": 178.5, "Name": "Hafnium" },
    { "Symbol": "Ta", "AtomicNumber": 73, "AtomicWeight": 180.9, "Name": "Tantalum" },
    { "Symbol": "W", "AtomicNumber": 74, "AtomicWeight": 183.9, "Name": "Tungsten" },
    { "Symbol": "Re", "AtomicNumber": 75, "AtomicWeight": 186.2, "Name": "Rhenium" },
    { "Symbol": "Os", "AtomicNumber": 76, "AtomicWeight": 190.2, "Name": "Osmium" },
    { "Symbol": "Ir", "AtomicNumber": 77, "AtomicWeight": 192.2, "Name": "Iridium" },
    { "Symbol": "Pt", "AtomicNumber": 78, "AtomicWeight": 195.1, "Name": "Platinum" },
    { "Symbol": "Au", "AtomicNumber": 79, "AtomicWeight": 197.0, "Name": "Gold" },
    { "Symbol": "Hg", "AtomicNumber": 80, "AtomicWeight": 200.5, "Name": "Mercury" },
    { "Symbol": "Tl", "AtomicNumber": 81, "AtomicWeight": 204.38, "Name": "Thallium" },
    { "Symbol": "Pb", "AtomicNumber": 82, "AtomicWeight": 207.2, "Name": "Lead" },
    { "Symbol": "Bi", "AtomicNumber": 83, "AtomicWeight": 209.0, "Name": "Bismuth" },
    { "Symbol": "Po", "AtomicNumber": 84, "AtomicWeight": 209, "Name": "Polonium" },
    { "Symbol": "At", "AtomicNumber": 85, "AtomicWeight": 210, "Name": "Astatine" },
    { "Symbol": "Rn", "AtomicNumber": 86, "AtomicWeight": 222, "Name": "Radon" },
    { "Symbol": "Fr", "AtomicNumber": 87, "AtomicWeight": 223, "Name": "Francium" },
    { "Symbol": "Ra", "AtomicNumber": 88, "AtomicWeight": 226, "Name": "Radium" },
    { "Symbol": "Ac", "AtomicNumber": 89, "AtomicWeight": 227, "Name": "Actinium" },
    { "Symbol": "Th", "AtomicNumber": 90, "AtomicWeight": 232, "Name": "Thorium" },
    { "Symbol": "Pa", "AtomicNumber": 91, "AtomicWeight": 231, "Name": "Protactinium" },
    { "Symbol": "U", "AtomicNumber": 92, "AtomicWeight": 238, "Name": "Uranium" },
    { "Symbol": "Np", "AtomicNumber": 93, "AtomicWeight": 237, "Name": "Neptunium" },
    { "Symbol": "Pu", "AtomicNumber": 94, "AtomicWeight": 244, "Name": "Plutonium" },
    { "Symbol": "Am", "AtomicNumber": 95, "AtomicWeight": 243, "Name": "Americium" },
    { "Symbol": "Cm", "AtomicNumber": 96, "AtomicWeight": 247, "Name": "Curium" },
    { "Symbol": "Bk", "AtomicNumber": 97, "AtomicWeight": 247, "Name": "Berkelium" },
    { "Symbol": "Cf", "AtomicNumber": 98, "AtomicWeight": 251, "Name": "Californium" },
    { "Symbol": "Es", "AtomicNumber": 99, "AtomicWeight": 252, "Name": "Einsteinium" },
    { "Symbol": "Fm", "AtomicNumber": 100, "AtomicWeight": 257, "Name": "Fermium" },
    { "Symbol": "Md", "AtomicNumber": 101, "AtomicWeight": 258, "Name": "Mendelevium" },
    { "Symbol": "No", "AtomicNumber": 102, "AtomicWeight": 259, "Name": "Nobelium" },
    { "Symbol": "Lr", "AtomicNumber": 103, "AtomicWeight": 262, "Name": "Lawrencium" },
    { "Symbol": "Rf", "AtomicNumber": 104, "AtomicWeight": 265, "Name": "Rutherfordium" },
    { "Symbol": "Db", "AtomicNumber": 105, "AtomicWeight": 268, "Name": "Dubnium" },
    { "Symbol": "Sg", "AtomicNumber": 106, "AtomicWeight": 271, "Name": "Seaborgium" },
    { "Symbol": "Bh", "AtomicNumber": 107, "AtomicWeight": 270, "Name": "Bohrium" },
    { "Symbol": "Hs", "AtomicNumber": 108, "AtomicWeight": 277, "Name": "Hassium" },
    { "Symbol": "Mt", "AtomicNumber": 109, "AtomicWeight": 276, "Name": "Meitnerium" },
    { "Symbol": "Ds", "AtomicNumber": 110, "AtomicWeight": 281, "Name": "Darmstadtium" },
    { "Symbol": "Rg", "AtomicNumber": 111, "AtomicWeight": 280, "Name": "Roentgenium" },
    { "Symbol": "Cn", "AtomicNumber": 112, "AtomicWeight": 285, "Name": "Copernicium" },
    { "Symbol": "Uut", "AtomicNumber": 113, "AtomicWeight": 284, "Name": "Ununtrium" },
    { "Symbol": "Fl", "AtomicNumber": 114, "AtomicWeight": 289, "Name": "Flerovium" },
    { "Symbol": "Uup", "AtomicNumber": 115, "AtomicWeight": 288, "Name": "Ununpentium" },
    { "Symbol": "Lv", "AtomicNumber": 116, "AtomicWeight": 293, "Name": "Livermorium" },
    { "Symbol": "Uus", "AtomicNumber": 117, "AtomicWeight": 294, "Name": "Ununseptium" },
    { "Symbol": "Uuo", "AtomicNumber": 118, "AtomicWeight": 294, "Name": "Ununoctium" }
];

var PeriodicTable = {
    SymbolFind : function(symbol) {
        symbol = symbol.toUpperCase();

        for (var i = 0; i < gPeriodicTable.length; i++) {
            if (gPeriodicTable[i].Symbol.toUpperCase() == symbol) {
                return gPeriodicTable[i];
            }
        }

        return null;
    },

    AtomicNumberFind : function(number) {
        for (var i = 0; i < gPeriodicTable.length; i++) {
            if (gPeriodicTable[i].AtomicNumber == number) {
                return gPeriodicTable[i];
            }
        }

        return null;
    },

    NameFind : function(name) {
        name = name.toUpperCase();

        for (var i = 0; i < gPeriodicTable.length; i++) {
            if (gPeriodicTable[i].Name.toUpperCase() == name) {
                return gPeriodicTable[i];
            }
        }

        return null;
    },
}