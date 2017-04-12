function CheckSession() {
    var id = App.direct.CheckSession({
        success: function (result) {

            if (result == "0") {

                window.open('ARLogin.aspx?timeout=yes', '_parent');
                return false;
            }

        }
    })
}


// Checks a string to see if it in a valid date format
// of (D)D/(M)M/(YY)YY and returns true/false
function isValidDate(d) {
    if (Object.prototype.toString.call(d) === "[object Date]") {
        // it is a date
        if (isNaN(d.getTime())) {  // d.valueOf() could also work
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return false;
    }
}



