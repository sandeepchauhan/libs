var MatchHighlighter = (function () {
    function MatchHighlighter(input) {
        this.input = input;
    }
    MatchHighlighter.prototype.highlight = function () {
    };
    return MatchHighlighter;
})();

var host = window.location.host;
var baseURLOfHomeController = "http://" + host + "/api";
function func1() {
    var input = document.getElementById("searchText").nodeValue;
    var highlighter = new MatchHighlighter(input);
}
//# sourceMappingURL=TextEditor.js.map
