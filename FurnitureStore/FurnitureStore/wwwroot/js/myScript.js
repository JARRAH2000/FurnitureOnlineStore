var loadFile = function (event) {
    var image = document.getElementById('output');
    image.src = URL.createObjectURL(event.target.files[0]);
    image.hidden = false;
    var button = document.getElementById('removebutton');
    button.hidden = false;
    
};
var dropFile = function (output, image) {
    var shower = document.getElementById(output);
    shower.src = "/Images/defaultUserImage.png";
    var image2 = document.getElementById(image);
    image2.value = null;
    var button = document.getElementById('removebutton');
    button.hidden = true;
};

var darkMode=function() {
    var element = document.body;
    element.classList.toggle("dark");
};


var dropImageMessage = function (output, image) {
    var shower = document.getElementById(output);
    shower.hidden = true;
    shower.src = "/Images/defaultMessageImage.jpeg";
    var image2 = document.getElementById(image);
    image2.value = null;
    var button = document.getElementById('removebutton');
    button.hidden = true;
};