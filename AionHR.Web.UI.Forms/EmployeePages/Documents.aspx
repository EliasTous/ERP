<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Documents" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/Documents.js?id=27"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../CSS/fileinput.min.css" rel="stylesheet" />
    <link href="../CSS/theme.css" rel="stylesheet" />

    <!-- load the JS files in the right order -->
    <script src="../Scripts/fileinput.js" type="text/javascript"></script>
    <script src="../Scripts/theme.js" type="text/javascript">  </script>
    <script src="../Scripts/moment.js" type="text/javascript">  </script>
    <script src="../Scripts/moment-timezone.js" type="text/javascript">  </script>

    <script type="text/javascript" src="../Scripts/locales/ar.js?id=7"></script>
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';

        }
        var types = [];
        var curIndex = 0;
        var passed = 'no';
        function InitTypes(s) {

            types = s;
        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
        function initBootstrap() {
            curIndex = 0;

            $("#input-ke-1").fileinput({

                theme: 'explorer',
                uploadUrl: '../SystemAttachmentsUploader.ashx?recordId=' + document.getElementById('CurrentEmployee').value + "&classId=" + document.getElementById("EmployeeClassId").value,
                overwriteInitial: false,
                initialPreviewAsData: true,
                uploadAsync: true,
                language: document.getElementById('CurrentLanguage').value,
                showZoom: false,
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
                    showUpload: true,
                    showZoom: false,



                },
                layoutTemplates: {

                    actionUpload: '<select type="text" name="values"  >\n' +

                        '</select>'

                },
                showUploadedThumbs: false


            });







            $('#input-ke-1').on('filebatchuploadcomplete', function (event, data, msg) {


                App.employeeDocumentsStore.reload();
                App.AttachmentsWindow.close();

            });
            $('#input-ke-1').on('fileloaded', function (event, file, previewId, index, reader) {
                var x = document.getElementsByName("values");

                var s = $("#" + previewId).find("select")[0];
                s.options.length = 0;
                for (var j = 0; j < types.length; j++) {
                    var opt = document.createElement('option');
                    opt.value = types[j].value;
                    opt.innerHTML = types[j].text;
                    s.appendChild(opt);
                }

            });
        }
        function  closewindow()
        {
            App.Window1.hide();
        }

    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="CurrentDateFormat" runat="server" />
        <ext:Hidden ID="EmployeeClassId" runat="server" />
        <ext:Hidden ID="CurrentLanguage" runat="server" />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
        <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>



            <Items>

                <ext:GridPanel AutoUpdateLayout="true"
                    ID="employeeDocumentsGrid" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false"
                    Icon="User" DefaultAnchor="100%"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="employeeDocumentsStore"
                            runat="server"
                            RemoteSort="False"
                            RemoteFilter="true"
                            OnReadData="employeeDocumentsStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model1" runat="server" IDProperty="seqNo">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="seqNo" />
                                        <ext:ModelField Name="fileName" />
                                        <ext:ModelField Name="url" />
                                        <ext:ModelField Name="date" />
                                        <ext:ModelField Name="folderId" />
                                        <ext:ModelField Name="folderName" />

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="seqNo" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{employeeDocumentsGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" Visible="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{employeeDocumentsStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" Visible="true" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentRef%>" DataIndex="fileName" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFolderName%>" DataIndex="folderName" Flex="2" Hideable="false" />
                            <ext:DateColumn ID="dateCol" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Flex="2" Hideable="false">
                                
                            </ext:DateColumn>



                           
                            <ext:Column runat="server"
                                ID="ColEHDelete" Flex="1" Visible="false"
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
                             <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="120"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="var d =(App.EmployeeTerminated.value=='0')?editRender()+'&nbsp;&nbsp;' +deleteRender():' '; return attachRender()+'&nbsp;&nbsp;'+previewRender()+'&nbsp;&nbsp;' +d;" />

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

                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP" IsUpload="true">
                            <EventMask ShowMask="false" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['url']" Mode="Raw" />
                                <ext:Parameter Name="fileName" Value="record.data['fileName']" Mode="Raw" />
                                <ext:Parameter Name="folderId" Value="record.data['folderId']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>

            </Items>
        </ext:Viewport>

        <ext:Window
            ID="EditDocumentWindow"
            runat="server"
            Icon="PageEdit"
             Draggable="false"
              Maximizable="false" Resizable="false"
            Width="300"
            Height="120"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                            ID="EditDocumentForm" DefaultButton="SaveDocumentButton"
                            runat="server"
                            
                             Header="false"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="recordId" ID="seqNo" Hidden="true" Disabled="true" />
                                <ext:TextField runat="server" Name="fileName" ID="fileName" Hidden="true" Disabled="true" />


                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="folderId" Name="folderId" FieldLabel="<%$ Resources:FieldFolderName%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="dtStore">
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
                                        <ext:Button ID="Button6" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addFolder">
                                                    <ExtraParams>
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                           </Items>

                        </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveDocumentButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditDocumentForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveFolder" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditDocumentWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{seqNo}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="fileName" Value="#{fileName}.getValue().replace(/[^0-9a-z%.]/gi, '')" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditDocumentForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Window ID="AttachmentsWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditDocumentWindowTitle %>"
            Width="600"
            Height="550"
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
                        <input id="input-ke-1" name="inputKE1[]" type="file" multiple class="file-loading">

                        <br>
                    </Content>
                    <Listeners>


                        <AfterLayout Handler=" $('#input-ke-1').fileinput('destroy'); initBootstrap(); " />

                    </Listeners>
                </ext:Panel>

            </Items>
        </ext:Window>

        
        <ext:Window ID="Window1"
            runat="server"
            Icon="PageEdit"
         
            Width="600"
            Height="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Maximizable="true"
            Resizable="true"
            Draggable="true"
            Layout="AutoLayout" Scrollable="Both" >
            <Items>
          <ext:FormPanel runat="server">
              <Items>
                  <ext:TextField runat="server" Hidden="true" ID="ImageUrl" Name="ImageUrl" />
                    <ext:Image runat="server" ID="imgControl"   >
                          

                        </ext:Image>
                  </Items>
              </ext:FormPanel>

            </Items>
             <Buttons>
              
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>


    </form>
</body>
</html>

