
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};



var commandName;
var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();



    var t = e.getTarget(),
        columnId = this.columns[columnIndex].id; // Get column id

    if (t.className == "imgEdit") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }

    if (t.className == "imgDelete") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className == "imgAttach") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }


    //forbidden
    return false;
};


var getCellType = function (grid, rowIndex, cellIndex) {
    if (cellIndex == 0)
        return "";
    if (commandName != "")
        return commandName;
    var columnId = grid.columns[cellIndex].id; // Get column id

    return columnId;
};


var enterKeyPressSearchHandler = function (el, event) {

    var enter = false;
    if (event.getKey() == event.ENTER) {
        if (el.getValue().length > 0)
        { enter = true; }
    }

    if (enter === true) {
        App.Store1.reload();
    }
};

var circle;
var rectangle;
var map;
var geocoder;
function setWidth() {
    console.log(App.mapHolder.getWidth());
    console.log(App.mapHolder.getHeight());
    document.getElementById("map").style.width = App.mapHolder.getWidth() + 4 + 'px';
    document.getElementById("map").style.height = App.mapHolder.getHeight() - 25 + 'px';

}
var drawingManager;
function initMap(addr) {

    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 12
    });
    geocoder = new google.maps.Geocoder();
    var edit = App.viewOnly.value == "True";
    var drag = edit;
    drawingManager = new google.maps.drawing.DrawingManager({

        drawingControl: true,
        drawingControlOptions: {
            position: google.maps.ControlPosition.TOP_CENTER,
            drawingModes: ['circle', 'rectangle']
        },

        circleOptions: {


            strokeWeight: 2,
            clickable: false,
            editable: !edit,
            zIndex: 1,
            draggable: !edit
        },
        rectangleOptions: {


            strokeWeight: 2,
            draggable: !edit,
            editable: !edit,
            zIndex: 1
        }
    });
    
    drawingManager.setMap(map);

    google.maps.event.addListener(drawingManager, 'overlaycomplete', function (e) {
        if (e.type == google.maps.drawing.OverlayType.CIRCLE) {
            circle = e.overlay;
        }
        else {
            rectangle = e.overlay;
        }
        // Switch back to non-drawing mode after drawing a shape.
        drawingManager.setDrawingMode(null);
        // To hide:
        drawingManager.setOptions({
            drawingControl: false
        });
        document.getElementById("delete").removeAttribute('disabled');

    }

);
    document.getElementById("delete").onclick = function () {
        if (rectangle != null) {
            rectangle.setMap(null);
            rectangle = null;
        }
        if (circle != null) {
            circle.setMap(null);
            circle = null;
        }

        drawingManager.setOptions({
            drawingControl: true
        });
        this.disabled = 'disabled';
    };

    if (circle != null || rectangle != null) {
        drawingManager.setOptions({
            drawingControl: false
        });
        document.getElementById("delete").removeAttribute('disabled');
    }
    geocodeAddress(addr);


}
function dump(obj) {
    var out = '';
    for (var i in obj) {
        out += i + ": " + obj[i] + "\n";


    }
    return out;
}

function getCircleJson() {

    if (circle == null) {

        return null;
    }
    return { lat: circle.center.lat(), lon: circle.center.lng(), radius: circle.radius };
}
function clearMap() {
    if (circle != null) {
        circle.setMap(null);
        circle = null;
    }
    if (rectangle != null) {
        rectangle.setMap(null);
        rectangle = null;
    }
    drawingManager.setOptions({
        drawingControl: true
    });
    document.getElementById("delete").disabled = 'disabled';
}
function getRectangleJson() {
    if (rectangle == null)
        return null;

    return { lat1: rectangle.bounds.getNorthEast().lat(), lat2: rectangle.bounds.getSouthWest().lat(), lon1: rectangle.bounds.getNorthEast().lng(), lon2: rectangle.bounds.getSouthWest().lng() };
}
function AddCircle(latitude, longitude, r) {
    rectangle = null;
    var edit = App.viewOnly.value == "True";
    circle = new google.maps.Circle({

        strokeOpacity: 2,
        strokeWeight: 0.5,
        editable: !edit,
        draggable: !edit,
        fillOpacity:0.4,

        center: { lat: latitude, lng: longitude },
        map: map,
        radius: r
    });
    drawingManager.setOptions({
        drawingControl: false
    });
    if (App.viewOnly.value != "True")
        document.getElementById("delete").removeAttribute('disabled');
    map.setCenter(circle.center);
    map.fitBounds(circle.getBounds());

}
function AddRectangle(lat1, lon1, lat2, lon2) {
    circle = null;
    rectangle = new google.maps.Rectangle({


        strokeWeight: 2,

        editable: !edit,
        draggable: !edit,
        map: map,
        bounds: {
            north: lat1,
            south: lat2,
            east: lon1,
            west: lon2
        }
    });
    drawingManager.setOptions({
        drawingControl: false
    });
    if (App.viewOnly.value != "True")
        document.getElementById("delete").removeAttribute('disabled');
    map.setCenter(rectangle.bounds.getCenter());
    map.fitBounds(rectangle.bounds);
}

function isCircle() {
    return circle != null;
}
function getLat1() {
    if (App.noAccess.value == 'True')
        return App.lat1.value;
    if (isCircle())
        return getCircleJson().lat;
    else
        return getRectangleJson().lat1;
}
function getLon1() {
    if (App.noAccess.value == 'True')
        return App.lon1.value;
    if (isCircle())
        return getCircleJson().lon;
    else
        return getRectangleJson().lon1;

}
function getLat2() {
    if (App.noAccess.value == 'True')
        return App.lat2.value;
    if (!isCircle())
        return getRectangleJson().lat2;
}
function getLon2() {
    if (App.noAccess.value == 'True')
        return App.lon2.value;
    if (!isCircle())

        return getRectangleJson().lon2;

}
function getRadius() {
    if (App.noAccess.value == 'True')
        return App.radius.value;
    if (isCircle())

        return getCircleJson().radius;

}

function geocodeAddress(addr) {

    geocoder.geocode({ 'address': addr }, function (results, status) {
        if (status === 'OK') {
            map.setCenter(results[0].geometry.location);
        }
    });
}