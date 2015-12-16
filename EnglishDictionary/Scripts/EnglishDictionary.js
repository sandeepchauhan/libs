var host = window.location.host;
var baseURLOfHomeController = "http://" + host + "/api";
var startTime;
var timeTaken;
var indentSpaces = "&nbsp;&nbsp;&nbsp;&nbsp;";

function func(event) {
    var input = document.getElementById("word").value;
    var key = event.which || event.keyCode;
    if (key == 13) {
        showMeaning(input);
    } else if (input.length > 2) {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    timeTaken = (new Date()).getTime() - startTime;
                    var jsList = JSON.parse(xmlhttp.responseText);
                    var html = "";
                    for (var i = 0; i < jsList.length; i++) {
                        html += "<li><a onclick=\"showMeaning('" + jsList[i] + "')\">" + jsList[i] + "</a></li>";
                    }
                    document.getElementById("suggestionsList").innerHTML = html;
                    document.getElementById("suggestionsList").style.visibility = "visible";
                    document.getElementById("suggestionsCallTimeTaken").innerHTML = "Time taken by last suggestions call: " + timeTaken;
                }
            }
        };
        xmlhttp.open("GET", baseURLOfHomeController + "/suggestions?word=" + input + "&timestamp=" + (new Date()).getTime(), true);
        startTime = (new Date()).getTime();
        xmlhttp.send();
    } else {
        document.getElementById("suggestionsList").style.visibility = "hidden";
    }
}

function showMeaning(word) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4) {
            if (xmlhttp.status == 200) {
                document.getElementById("suggestionsList").style.visibility = "hidden";
                document.getElementById("lastlookup").innerHTML = singleWordToCamelCase(word) + ": " + xmlhttp.responseText;
            } else if (xmlhttp.status == 404) {
                document.getElementById("lastlookup").innerHTML = "No result found, did you mean any of the following?<br/><br/>";
                showCorrections(word);
            }
        }
    };

    var url = baseURLOfHomeController + "/meaning?word=" + word + "&timestamp=" + (new Date()).getTime();
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function showCorrections(word) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4) {
            if (xmlhttp.status == 200) {
                timeTaken = (new Date()).getTime() - startTime;
                var responseObj = JSON.parse(xmlhttp.responseText);
                var corrections = responseObj["m_Item1"];
                var html = "";
                for (var i = 0; i < corrections.length; i++) {
                    html += "<li><a onclick=\"showMeaning('" + corrections[i] + "')\">" + corrections[i] + "</a></li>";
                }
                document.getElementById("suggestionsList").innerHTML = html;
                document.getElementById("suggestionsList").style.visibility = "visible";
                document.getElementById("suggestionsCallTimeTaken").innerHTML = "Time taken by last suggestions call: " + timeTaken;
                var perfDataObj = responseObj["m_Item2"];
                document.getElementById("perfData").innerHTML = printJSONObject(perfDataObj, 0);
            }
        }
    };
    xmlhttp.open("GET", baseURLOfHomeController + "/corrections?word=" + word + "&timestamp=" + (new Date()).getTime(), true);
    startTime = (new Date()).getTime();
    xmlhttp.send();
}

function singleWordToCamelCase(word) {
    var length = word.length;
    var firstChar = word.charAt(0);
    return firstChar.toUpperCase() + word.toLowerCase().substr(1, length - 1);
}

function printJSONObject(obj, indentationLevel) {
    var ret = "";
    var linePrefix = "";
    for (var j = 0; j < indentationLevel; j++) {
        linePrefix += indentSpaces;
    }
    var propertyNames = Object.getOwnPropertyNames(obj);
    var complexPropertyNames = new Array();
    for (var i = 0; i < propertyNames.length; i++) {
        if (typeof (obj[propertyNames[i]]) === "object") {
            complexPropertyNames.push(propertyNames[i]);
        } else {
            ret += linePrefix + propertyNames[i] + ": " + obj[propertyNames[i]] + "<br/>";
        }
    }
    for (var i = 0; i < complexPropertyNames.length; i++) {
        if (Object.getOwnPropertyNames(obj[complexPropertyNames[i]]).length > 0) {
            ret += linePrefix + complexPropertyNames[i] + ":<br/>";
            ret += printJSONObject(obj[complexPropertyNames[i]], indentationLevel + 1);
        }
    }

    return ret;
}
//# sourceMappingURL=EnglishDictionary.js.map
