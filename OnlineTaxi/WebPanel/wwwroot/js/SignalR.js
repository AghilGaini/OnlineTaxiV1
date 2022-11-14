var connection = new signalR.HubConnectionBuilder().withUrl("/RequestTrip").build();
connection.start().then(function () {
    console.log("Signalr conected");
}).catch(function (err) {
    return console.error(err.toString());
});

function SendTestMsg() {
    var message = "This is Test Message";
    connection.invoke("SendMsgToAll", message).then(function () {
        console.log("Success call");
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("RecieveMsg", function (msg) {
    alert(msg);
});

connection.on("SignalRNotification", function (msg) {
    alert(msg);
});