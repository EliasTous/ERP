<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyFiles.aspx.cs" Inherits="AionHR.Web.UI.Forms.CompanyFiles" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/CompanyFiles.js?id=2" ></script>
   <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    
   
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    


    <script type="text/javascript" src="Scripts/common.js" ></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
     <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>
 
     <link href="CSS/fileinput.min.css" rel="stylesheet" />
<link href="CSS/theme.css" rel="stylesheet" />
 
<!-- load the JS files in the right order -->
<script src="Scripts/fileinput.js" type="text/javascript"></script>
<script src="Scripts/theme.js" type="text/javascript">  </script>
    <script src="Scripts/moment.js" type="text/javascript">  </script>
    <script src="Scripts/moment-timezone.js" type="text/javascript">  </script>

     <script type="text/javascript" src="Scripts/locales/ar.js?id=7" ></script>
 <script type="text/javascript">
     var types = [];
     var curIndex = 0;
     var passed = 'no';
     function InitTypes(s)
     {
         
         types = s;
     }
     function dump(obj) {
         var out = '';
         for (var i in obj) {
             out += i + ": " + obj[i] + "\n";


         }
         return out;
     }
     function initBootstrap()
     {
         curIndex = 0;
        
         $("#input-ke-1").fileinput({
           
             theme: 'explorer',
             uploadUrl: 'SystemAttachmentsUploader.ashx?recordId=0&classId=' +document.getElementById("CompanyFilesClassId").value ,
             overwriteInitial: false,
             initialPreviewAsData: true,
             uploadAsync: true,
             language:document.getElementById('CurrentLanguage').value,
             showZoom:false,
             showRemove: false,
           //  uploadExtraData: { id: $($(this).find('select')[0]).val() },
             uploadExtraData: function (previewId, index) {

                var valType = $($("#" + previewId).find('select')[0]).val();
                 return { id: valType };
              //   var obj = {};
             //    $(this).find('select').each(function () {
              //       var id = 'valType', val = $(this).val();
              //       obj[id] = val;
              //   });
              //   console.log(obj);
               //  return obj;
                
                // var extra = [];
               //  { typeValue: $('#id').val() };
                 //alert(curIndex);
                 //var x = document.getElementsByName("values");
                 
                 //var ext = { id: x[curIndex].value };
                 //if(passed=='yes')
                 //    curIndex = curIndex + 1;
                 //passed = 'yes';
                 //return extra;
             },
             fileActionSettings: {
                 showDrag: false,
                 showUpload:true,
                 showZoom: false,
              
                     
                 
             },
             layoutTemplates: {
               
                 actionUpload: '<select type="text" name="values"  >\n'+
                     
                     '</select>'
          
             }  ,
             showUploadedThumbs: false
           
             
         });
         
         
         $('#input-ke-1').on('filepreupload', function (event, data, previewId, index) {
             var form = data.form, files = data.files, extra = data.extra,
                 response = data.response, reader = data.reader;
             console.log($(this).find('select')[0]);
         });
       


         $('#input-ke-1').on('filebatchuploaderror', function (event, data, msg) {
             var form = data.form, files = data.files, extra = data.extra,
                 response = data.response, reader = data.reader;
             console.log('File batch upload error');
             // get message
             alert(msg);
         });
         $('#input-ke-1').on('filebatchuploadcomplete', function (event, data, msg) {
             
             App.direct.FillFilesStore(document.getElementById('currentCase').value);
             App.AttachmentsWindow.close();
             
         });
         $('#input-ke-1').on('filebatchpreupload', function (event, data, previewId, index) {
             var form = data.form, files = data.files, extra = data.extra,
                 response = data.response, reader = data.reader;
             console.log('File batch pre upload');
         });
         $('#input-ke-1').on('fileloaded', function (event, file, previewId, index, reader) {
             var x = document.getElementsByName("values");
          
             var s = $("#" + previewId).find("select")[0];
            
                 for(var j=0;j<types.length;j++)
                 {
                     var opt = document.createElement('option');
                     opt.value = types[j].value;
                     opt.innerHTML =  types[j].text;
                     s.appendChild(opt);
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
        <ext:Hidden ID="CurrentLanguage" runat="server" />
        <ext:Hidden ID="CompanyFilesClassId" runat="server" />
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
                        <ext:ModelField Name="folderId" />
                        <ext:ModelField Name="folderName" />
                        <ext:ModelField Name="date" />
                        <ext:ModelField Name="url" />
                        <%--<ext:ModelField Name="reference" />--%>
                      
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
                            <ext:Column    CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                        </ext:Column>
                                 <ext:Column    CellCls="cellLink" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFolder%>" DataIndex="folderId" Flex="2" Hideable="false">
                        </ext:Column>
                              <ext:DateColumn  ID="dateCol" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Flex="2" Hideable="false" >
                                <Renderer Handler="var s = moment(record.data['date']);   return s.calendar();" />
                                </ext:DateColumn>
                            <%--<ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldReference%>" DataIndex="reference" Width ="300" Hideable="false" />--%>
                             
                        
                           

                          <ext:Column runat="server"
                                ID="Column8" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return attachRender() + '&nbsp&nbsp' + deleteRender(); " />
                              </ext:Column>
                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
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

        

        

        <ext:Window  ID="AttachmentsWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="600"
            Height="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
             Maximizable="false"
            Resizable="false"
             Draggable="false"
            Layout="Fit">
            <Items>
                <ext:Panel runat="server" AutoScroll="true">
                    <Content>
                        <input id="input-ke-1" name="inputKE1[]" type="file" multiple class="file-loading"  >
                        <input type="hidden" name="caseId" id="caseId" value="" />
        <br>
      
                    </Content>
                    <Listeners>
                      
                        
                        <AfterLayout Handler=" $('#input-ke-1').fileinput('destroy'); initBootstrap(); document.getElementById('caseId').value = document.getElementById('currentCase').value;" />
                       
                    </Listeners>
                </ext:Panel>

            </Items>
        </ext:Window>

    </form>
</body>
</html>