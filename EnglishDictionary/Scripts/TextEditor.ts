class MatchHighlighter {
    input: string;

    constructor(input: string) {
        this.input = input;
    }

    highlight() {

    }
}

var host = window.location.host;
var baseURLOfHomeController = "http://" + host + "/api";
function func() {
    var input = document.getElementById("searchText").nodeValue;
    var highlighter = new MatchHighlighter(input);

}