
var DeleteDaySchedule = function (id) {
       
    $('[id^="'+id+'_"]').each(function () {
        
        $(this).css("background-color", "white");
    })
};



var ColorifyAndCountSchedule = function (listIds) {

    for (var i in listIds) {
        try {

       
            //$("[id='" + listIds[i].id + "']").css("background-color", "green");
            $("[id='" + listIds[i].id + "']").html(listIds[i].count);
            
        }
        catch (e) { }
    }
};

var BranchAvailability =  function()
{
    //App.pnlTools.hide();
    App.btnSave.setDisabled(true);
    App.btnDeleteDay.setDisabled(true);
    App.btnDelete.setDisabled(true);
   // App.btnClear.setDisabled(true);
}

var ColorifySchedule = function (listIds) {
       
        for(var i in listIds)
        {
            try {
                document.getElementById(listIds[i]).style.background = "green";
            }
            catch (e) { }
        }
};

var Init = function () {
   // App.pnlTools.show();
    App.btnClear.setDisabled(false);
      $('.day').each(function() { 
           $(this).click(function() {
  clearDayClick();
  $(this).css("background-color", "orange");
 
  App.dayId.setValue($(this)["0"].id);  
  App.btnSave.setDisabled(false);
  App.btnDeleteDay.setDisabled(false);
  });
      });
      }

 var DisableTools = function () {
     App.btnSave.setDisabled(true);
     App.btnDeleteDay.setDisabled(true);
 }
 var EnableTools = function () {
     App.btnSave.setDisabled(false);
     App.btnDeleteDay.setDisabled(false);
 }

var clearDayClick =function() {
        $('.day').each(function() {
           $(this).css("background-color","#fff");
  });
}





    //Issa
var colorify = function (tdID, color,date,dow) {
    $("#" + tdID).attr("style", "background:" + color);
    $("#" + tdID).attr("title", dow+" -"+ date);
};


var init = function () {
    $('#tbCalendar td').each(function () {
        $(this).css('backgroundColor', '#ffffff');
    });
};
var setLeapDay = function () {
    
    
   
    $("#td0229").addClass("notexist");
        $("#td0229").html("X");
        
    

}
function getDay(dow) {

    switch (dow) {
        case 1: return document.getElementById('MondayText').value;
        case 2: return document.getElementById('TuesdayText').value;
        case 3: return document.getElementById('WednesdayText').value;
        case 4: return document.getElementById('ThursdayText').value;
        case 5: return document.getElementById('FridayText').value;
        case 6: return document.getElementById('SaturdayText').value;
        case 7: return document.getElementById('SundayText').value;
    }
}
$(document).ready(function () {
    $("#tbCalendar td").click(function () {

        if (!$(this).hasClass('notexist')) {
            currentDayId = $(this).find('.hidden:first').html();

            App.direct.OpenDayConfig(currentDayId);
        }
    });
});
