<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseCalculator.aspx.cs" Inherits="PHTWeb.BaseCalculator" %>
<!--
Featue Todo:

    Add ability to have multiple rows of equations.

-->


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <script type="text/javascript">
        var operatorHtml =
            '<select onkeydown="return doOperationKeyDown(event);" onchange="doUpdateCalculations();" title="Use Insert and Delete keys to add and remove operations.">' +
            '<option>+</option>' +
            '<option>-</option>' +
            '<option>*</option>' +
            '<option>/</option>' +
            '<option>=</option>' +
            '</select>';
        


        function doUpdateCalculations() {
            var table = document.getElementById("EquationTable");

            if (table) {
                for (var i = 0; i < table.rows.length; i++) {
                    var row = table.rows[i];
                    var operation = '+';
                    var value = 0;
                    var accum = 0;
                    for (var j = 0; j < row.cells.length; j++) {
                        var cell = row.cells[j];

                        if ((j % 2) == 0) {
                            var base = parseInt(cell.children[1].children[0].value, 10);
                            value = parseInt(cell.children[0].children[0].value, base);
                            if (isNaN(value)) {
                                value = 0;
                            }

                            switch (operation[i]) {
                                case '+': {
                                    accum = accum + value;
                                    break;
                                }
                                case '-': {
                                    accum = accum - value;
                                    break;
                                }
                                case '*': {
                                    accum = accum * value;
                                    break;
                                }
                                case '/': {
                                    if (value != 0) {
                                        accum = accum / value;
                                    }
                                    break;
                                }
                                default: {
                                    break;
                                }
                            }

                        }
                        else {
                            operation = cell.children[0].options[cell.children[0].selectedIndex].text;
                            if (operation[i] == '=') {
                                var radix = parseInt(row.cells[cell.cellIndex + 1].children[1].children[0].value, 10);
                                if (isNaN(radix) || radix <= 1) {
                                    radix = 10;
                                }
                                row.cells[cell.cellIndex + 1].children[0].children[0].value = accum.toString(radix);
                                break;
                            }
                        }
                    }
                }
            }
        }

        function doOperationKeyDown(event) {
            var result = true;
            var cell = event.currentTarget.parentElement;
            var row = cell.parentElement;

            if (event.keyCode == 45) {  // Insert
                valueCell = row.insertCell(cell.cellIndex);
                valueCell.innerHTML = row.cells[valueCell.cellIndex - 1].innerHTML;
                valueCell.children[0].children[0].value = "";

                operationCell = row.insertCell(valueCell.cellIndex);
                operationCell.innerHTML = operatorHtml;
                operationCell.children[0].focus();

                result = false;
            }
            else if (event.keyCode == 46) { // Delete
                var index = cell.cellIndex;
                row.deleteCell(index);
                row.deleteCell(index);

                row.cells[index].children[0].focus();

                result = false;
            }

            if (!result) {
                doUpdateCalculations();
            }

            return result;
        }

        function doEqualsKeyDown(event) {
            var result = true;
            var cell = event.currentTarget.parentElement;
            var row = cell.parentElement;

            if (event.keyCode == 45) {  // Insert
                valueCell = row.insertCell(cell.cellIndex);
                valueCell.innerHTML = row.cells[valueCell.cellIndex - 1].innerHTML;
                valueCell.children[0].children[0].value = "";

                operationCell = row.insertCell(valueCell.cellIndex);
                operationCell.innerHTML = operatorHtml;
                operationCell.children[0].focus();

                result = false;
            }

            if (!result) {
                doUpdateCalculations();
            }

            return result;
        }

    </script>
</head>
<body>
    <table id="EquationTable">
        <tr>
            <td>
                <div><input type="text" value="" size="2" onkeyup="doUpdateCalculations(); return true;"/></div>
                <div style="text-align:right;"><input type="text" value="10" size="1" style="text-align:right;font-size:x-small"  onkeyup="doUpdateCalculations(); return true;" /></div>
            </td>
            <td>
                <select onkeydown="return doEqualsKeyDown(event);" title="Use Insert and Delete keys to add and remove operations.">
                    <option>=</option>
                </select>
            </td>
            <td>
                <div><input type="text" disabled="disabled" value="0" size="2"/></div>
                <div style="text-align:right;"><input type="text" value="10" size="1" style="text-align:right;font-size:x-small"  onkeyup="doUpdateCalculations(); return true;" /></div>
            </td>
        </tr>
    </table>
</body>
</html>
