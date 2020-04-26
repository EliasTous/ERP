
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};
function dump(obj) {
    var out = '';
    for (var i in obj) {
        out += i + ": " + obj[i] + "\n";


    }
    return out;
}


var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();

    
 
    var t = e.getTarget(),
        columnId = this.columns[columnIndex].id; // Get column id

    if (t.className == "imgEdit" && columnId == "colEdit") {
        //the ajax call is allowed

        return true;
    }

    if (t.className == "imgDelete" && columnId == "colDelete") {
        //the ajax call is allowed
        return true;
    }
    if (t.className == "imgAttach" && columnId == "colAttach") {
        //the ajax call is allowed
        return true;
    }
    if (columnId == "ColName")
        return true;


    //forbidden
    return false;
};


var getCellType = function (grid, rowIndex, cellIndex) {
   
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
var DeptsData;
function Init( data)
{
    
  google.charts.load('current', { packages: ["orgchart"] });
    //google.charts.load('current', { packages: ["table"] });
    google.charts.setOnLoadCallback(drawChart);
    DeptsData = data;
}
var chart;
function drawChart() {
    try {

       
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Name');
        data.addColumn('string', 'Parent');
        data.addColumn('string', 'ToolTip');
        
        for (i = 0 ; i < DeptsData.length; i++) {
            var row = [];
           
            row.push(DeptsData[i].name);
            
            row.push(DeptsData[i].parent);
            row.push('');
            data.addRow(row);
           
            if (DeptsData[i].type == 2) {
                data.setRowProperty(i, 'style', 'background:-webkit-gradient(linear, 0% 0%, 0% 100%, from(rgba(88, 197, 32, 0.63)), to(rgb(105, 230, 120)));border:1px solid #4acc4a');
                
            }
        

        }

        //var chartr = new google.visualization.Table(document.getElementById('cc'));
        //chartr.draw(data, { width: 500, height: 300 });
        // For each orgchart box, provide the name, manager, and tooltip to show.


        
        // Create the chart.
        chart = new google.visualization.OrgChart(document.getElementById('chart_div'));
        // Draw the chart, setting the allowHtml option to true for the tooltips.
       chart.draw(data, { allowHtml: true , allowCollapse:true});
    }
    catch (e) { }
}

function PrintElem(elem) {
    print(chart);
    //var elem ="<img id='imgout'></img>";
    //html2canvas($("#chart_div"), {
    //    onrendered: function (canvas) {
    //        var img = canvas.toDataURL("image/png");
           
    //        var mywindow = window.open('', 'PRINT', 'height=400,width=600');
            
            
            
    //        mywindow.document.write('<html><head><title>' + document.title + '</title>');
           
    //        mywindow.document.write('</head><body >');
    //        mywindow.document.write('<h1>' + document.title + '</h1>');
    //        mywindow.document.write('<img src="' + img + '"/>');
    //        mywindow.document.write('</body></html>');

    //        mywindow.document.close(); // necessary for IE >= 10
    //        mywindow.focus(); // necessary for IE >= 10*/

    //        mywindow.print();
    //        mywindow.close();
    //    }
    //});
  
    
    return true;
}
