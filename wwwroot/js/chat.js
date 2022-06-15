"use strict";


var connection = new signalR.HubConnectionBuilder()
    .withUrl("/ChatMessage")
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("Receive", function (user, messages, avatar, sender) {
    var divconteiner = document.createElement("div");
    var divcontent = document.createElement("div");
    var img = document.createElement("img");
    var p = document.createElement("p");
    const cls_divconteiner = ["flex-row", "justify-content-start", "mb-4"];
    const cls_img = ["rounded-circle"];
    const cls_divcontent = ["ms-3", "left"];
    const cls_p = ["mb-0"];

    divconteiner.className = "d-flex";
    divconteiner.classList.add(...cls_divconteiner);
 
    img.className = "image-avatar";
    img.classList.add(...cls_img);

    divcontent.className = "p-3";
    divcontent.classList.add(...cls_divcontent);

    p.className = "small";
    p.classList.add(...cls_p);

    divconteiner.id = "divconteiner";
    divcontent.id = "divcontent"
    img.id = "img";
    p.id = "p"

    p.textContent = `${messages.content}`;
    img.src = `data:image/jpeg;base64,${avatar}`;
   
    //left
    if (user == sender) {
        divconteiner.classList.remove("justify-content-start");
        divconteiner.classList.add("justify-content-end");
        divcontent.classList.remove("ms-3");
        divcontent.classList.add("me-3");
        divcontent.classList.add("right");
        document.getElementById("chatmessage").appendChild(divconteiner);
        divconteiner.appendChild(divcontent);
        divcontent.appendChild(p);
        divconteiner.appendChild(img); 
    } //right
    else
    {
        document.getElementById("chatmessage").appendChild(divconteiner);
        divconteiner.appendChild(img);
        divcontent.classList.remove("right");
        divcontent.classList.add("left");
        divconteiner.appendChild(divcontent);
        divcontent.appendChild(p);     
    }
});

connection.on('Notify', function (message) {

    // добавляет элемент для диагностического сообщения
    let notifyElem = document.createElement("li");
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement("li");
    elem.appendChild(notifyElem);
    var firstElem = document.getElementById("messagesList").firstChild;
    document.getElementById("messagesList").insertBefore(elem, firstElem);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var messageInput = document.getElementById("messageInput");
    var message = messageInput.value;

    var sendButton = document.getElementById("sendButton");
    var userTo = sendButton.name;

    connection.invoke("SendMessage", message, userTo ).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});