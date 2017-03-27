<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Geofences.aspx.cs" Inherits="AionHR.Web.UI.Forms.Geofences" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
     <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRQ7sZoJrjEBuIBret1gCccSwicDusM3w&libraries=drawing"></script>
   
    <script type="text/javascript" src="Scripts/Geofences.js?id=2" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
    <script type="text/javascript">
        var circle;
        var rectangle;
        var map;
        var drawingManager;
        function initMap() {

            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -34.397, lng: 150.644 },
                zoom: 12
            });

            drawingManager = new google.maps.drawing.DrawingManager({

                drawingControl: true,
                drawingControlOptions: {
                    position: google.maps.ControlPosition.TOP_CENTER,
                    drawingModes: ['circle', 'rectangle']
                },

                circleOptions: {
                   
                    
                    strokeWeight: 2,
                    clickable: false,
                    editable: true,
                    zIndex: 1,
                    draggable:true
                },
                rectangleOptions: {
                
                
                strokeWeight: 2,
                draggable:true,
                editable: true,
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
            
            if (circle != null || rectangle != null)
            {
                drawingManager.setOptions({
                    drawingControl: false
                });
                document.getElementById("delete").removeAttribute('disabled');
            }
            
            

        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
      
        function getCircleJson()
        {
            
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
            if(rectangle!=null)
            {
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
        function AddCircle(latitude, longitude, r)
        {
            rectangle = null;
            circle = new google.maps.Circle({
                
                strokeOpacity:1,
                strokeWeight: 2,
                
                fillOpacity: 0.35,
              
                center: { lat: latitude, lng: longitude },
                map:map,
                radius: r
            });
            drawingManager.setOptions({
                drawingControl: false
            });
            document.getElementById("delete").removeAttribute('disabled');
            map.setCenter(circle.center);

        }
        function AddRectangle(lat1,lon1,lat2,lon2)
        {
            circle = null;
            rectangle = new google.maps.Rectangle({
                
                
                strokeWeight: 2,
                
                
                map:map,
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
            document.getElementById("delete").removeAttribute('disabled');
            map.setCenter(rectangle.bounds.getCenter());
        }

        function isCircle()
        {
            return circle != null;
        }
        function getLat1()
        {
            if (isCircle())
                return getCircleJson().lat;
            else
                return getRectangleJson().lat1;
        }
        function getLon1() {
            if (isCircle())
                return getCircleJson().lon;
            else
                return getRectangleJson().lon1;

        }
        function getLat2() {
            if (!isCircle())
                return getRectangleJson().lat2;
        }
        function getLon2() {
            if (!isCircle())
               
                return getRectangleJson().lon2;

        }
        function getRadius() {
            if (isCircle())

                return getCircleJson().radius;

        }

        function geocodeAddress(geocoder, resultsMap) {
            var address = document.getElementById('address').value;
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status === 'OK') {
                    resultsMap.setCenter(results[0].geometry.location);
                }
            });
            }
    </script>
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />                       
                        <ext:ModelField Name="name" />
                        <ext:ModelField Name="branchId" />
                        <ext:ModelField Name="branchName" />
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>


    
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:GridPanel 
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1" 
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"  
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button Visible="false" ID="btnDeleteSelected" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                 <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                 <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{Store1}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="4" Hideable="false">
                            <Renderer Handler="return '<u>'+ record.data['name']+'</u>'">

                            </Renderer>
                                </ext:Column>

                            <ext:Column ID="ColBranchName" DataIndex="branchName" Text="<%$ Resources: FieldBranchName%>" runat="server" Flex="4" />
                        
                           

                            <ext:Column runat="server"
                                ID="colEdit"  Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colDelete" Flex="1" Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />
                              
                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colAttach"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>




                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar2" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar1" runat="server" />
                                <ext:ToolbarFill />
                                
                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar1"
                            runat="server"
                            FirstText="<%$ Resources:Common , FirstText %>"
                            NextText="<%$ Resources:Common , NextText %>"
                            PrevText="<%$ Resources:Common , PrevText %>"
                            LastText="<%$ Resources:Common , LastText %>"
                            RefreshText="<%$ Resources:Common ,RefreshText  %>"
                            BeforePageText="<%$ Resources:Common ,BeforePageText  %>"
                            AfterPageText="<%$ Resources:Common , AfterPageText %>"
                            DisplayInfo="true"
                            DisplayMsg="<%$ Resources:Common , DisplayMsg %>"
                            Border="true"
                            EmptyMsg="<%$ Resources:Common , EmptyMsg %>">
                            <Items>
                               
                            </Items>
                            <Listeners>
                                <BeforeRender Handler="this.items.removeAt(this.items.length - 2);" />
                            </Listeners>
                        </ext:PagingToolbar>

                    </BottomBar>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>

                  
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>

        

        <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="470"
            AutoShow="false" 
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>

                                <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranchId%>" SimpleSubmit="true">
                                                    <Store>
                                                        <ext:Store runat="server" ID="BranchStore">
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                  <RightButtons>
                                                        <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true" >
                                                            <Listeners>
                                                                <Click Handler="CheckSession();" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addBranch">
                                                                 
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                    <Listeners>
                                                        <FocusEnter Handler="if(!this.readOnly) this.rightButtons[0].setHidden(false);" />
                                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                <ext:Panel runat="server" Layout="FormLayout" Height="500" Width="400" >
                                    <Items>
                                        <ext:Container runat="server">
                                        <Content>
                                        <div id="map" style=" height: 280px;width:400px;"></div>
                                            <input type="button" id="delete" value="Clear" disabled="disabled"/>
                                    </Content>
                                            
                                        </ext:Container>
                                    </Items>
                                  
                                    <Listeners>
                                        <AfterRender Handler="initMap()" />
                                    </Listeners>
                                </ext:Panel>
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                
                                <ext:Parameter Name="isCircle" Value="isCircle()" Mode="Raw" />
                                <ext:Parameter Name="lat1" Value="getLat1()" Mode="Raw" />
                                <ext:Parameter Name="lon1" Value="getLon1()" Mode="Raw" />
                                <ext:Parameter Name="lat2" Value="getLat2()" Mode="Raw" />
                                <ext:Parameter Name="lon2" Value="getLon2()" Mode="Raw" />
                                <ext:Parameter Name="radius" Value="getRadius()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>



    </form>
</body>
</html>
