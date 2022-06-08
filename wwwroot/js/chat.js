"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/ChatMessage")
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `${user} : ${message}`;
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