function changeIframeSrc_a() {
    var url = document.getElementById("input_url_a").value;
    var regex = /src="([^"]*)"/;
    var match = regex.exec(url);
    var src = match[1];
    document.getElementById("iframe_a").src = src;
}
function changeIframeSrc_b() {
    var url = document.getElementById("input_url_b").value;
    var regex = /src="([^"]*)"/;
    var match = regex.exec(url);
    var src = match[1];
    document.getElementById("iframe_b").src = src;
}
function fold_a() {
    var target_div = document.getElementById("iframe_div_a");
    var outer_div = document.getElementById("outer_div_a");
    if (target_div.style.display === "none") {
        target_div.style.display = "flex";
        outer_div.style.height = "100%";
    } else {
        target_div.style.display = "none";
        outer_div.style.height = "fit-content";
    }
    getscreensize();
}
function fold_b() {
    var target_div = document.getElementById("iframe_div_b");
    var outer_div = document.getElementById("outer_div_b");
    if (target_div.style.display === "none") {
        target_div.style.display = "flex";
        outer_div.style.height = "100%";
    } else {
        target_div.style.display = "none";
        outer_div.style.height = "fit-content";
    }
    getscreensize();
}

//const heightOutput = document.querySelector("#height");
//const widthOutput = document.querySelector("#width");
const container = document.querySelector('.container');
function getscreensize() {
    //heightOutput.textContent = window.innerHeight;
    //widthOutput.textContent = window.innerWidth;

    var div_a = document.getElementById("iframe_div_a");
    var div_b = document.getElementById("iframe_div_b");
    if (window.innerHeight * 3 < window.innerWidth * 2) {
        if (div_a.style.display === "none" || div_b.style.display === "none") {
            container.style.flexDirection = "column";
        } else {
            container.style.flexDirection = "row";
        }
    } else {
        container.style.flexDirection = "column";
    }
}
window.onresize = getscreensize;