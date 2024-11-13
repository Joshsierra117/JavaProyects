<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MT02.16-14.aspx.vb" Inherits="MT02_16_14" %>
<%@ Register Src="../../../_private/myGlobalD.ascx" TagName="myGlobal" TagPrefix="myGlobal" %>
<%@ Register Assembly="C1.Web.C1WebGrid.2" Namespace="C1.Web.C1WebGrid" TagPrefix="C1WebGrid" %>
<%@ Register Assembly="C1.Web.C1WebSplitter.2" Namespace="C1.Web.C1WebSplitter" TagPrefix="C1WebSplitter" %>
<%@ Register Src="../../../UT/myHeader.ascx" TagName="myHeader" TagPrefix="uc1" %>
<%@ Register Src="../../../UT/myFooter.ascx" TagName="myFooter" TagPrefix="uc2" %>
<%@ Register Src="~/UT/mySidebar.ascx" TagName="mySidebar" TagPrefix="mySidebar" %>
<%@ Register Src="~/UT/myHeaderSm.ascx" TagPrefix="uc2" TagName="myHeaderSm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <link href="../../../scripts/bootstrap/css/normalize.min.css" rel="stylesheet" />
    <link href="../../../scripts/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../../../styles/<%=myGlobal.GetPageStyle(Session("ent_index"))%>/bootstrap.custom.min.css" rel="stylesheet" />
    <link rel="Shortcut Icon" href="../styles/<%=myGlobal.GetPageStyle(Session("ent_index"))%>/Images/favicon.ico" />
    <link href="../../../styles/<%=myGlobal.GetPageStyle(Session("ent_index"))%>/Style.css" type="text/css" rel="stylesheet" />
    <link href="../../../scripts/bootstrap/plugins/datepicker/css/bootstrap-datepicker3.css" rel="stylesheet" />
    <title><%=myGlobal.GetTitle(2)%></title>
    <script src="../../../scripts/jquery-1.9.1.min.js"></script>
    <script src="../../../scripts/jquery-ui-1.10.3/ui/minified/jquery-ui.min.js"></script>
    <script src="../../../scripts/myGlobal_JS.js"></script>
    <script type="text/javascript" src="../../../scripts/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../../../scripts/bootstrap/plugins/datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="../../../scripts/bootstrap/plugins/datepicker/js/locales/bootstrap-datepicker.es.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" integrity="sha512-iBBXm8fW90+nuLcSKlbmrPcLa0OT92xO1BIsZ+ywDWZCvqsWgccV3gFoRBv0z+8dLJgyAHIhR35VZc2oM/gI1w==" crossorigin="anonymous" />
        <style type="text/css">
        html {height: 100%;}
        body {height: 100%; margin: 0; padding: 0;}
        #form1 {height: 100%; margin: 0; padding: 0;}
        #content {height: 100%; margin: 0; padding: 0;}
        th {text-align: center;}
    </style>
</head>
<body id="body" runat="server">
    <form id="form1" runat="server">
        <uc2:myHeaderSm runat="server" ID="myHeaderSm" />
        <div class="content" style="margin-left: 5px; margin-top: 10px;">
            <div class="HeaderStatic">
                <myGlobal:myGlobal ID="myGlobal" runat="server"></myGlobal:myGlobal>
                <%--<uc1:myHeader ID="myHeader1" runat="server" />--%>
                <table style="FONT-SIZE: 10pt; FONT-FAMILY: 'Trebuchet MS';" cellspacing="0" cellpadding="4" width="98.5%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lbDateRange1" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Fecha de: " AssociatedControlID="tbFromDate"></asp:Label>
                                <asp:RegularExpressionValidator ID="reDate1" runat="server"
                                        Text="Error" Display="Dynamic"
                                        ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31/((0[13578])|(1[02])))|((29|30)/((0[1,3-9])|(1[0-2])))))/((20[0-9][0-9]))|((((0[1-9])|(1\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31/((0[13578])|(1[02])))|((29|30)/((0[1,3-9])|(1[0-2])))))/((19[0-9][0-9]))|(29/02/20(([02468][048])|([13579][26])))|(29/02/19(([02468][048])|([13579][26]))))$"
                                        ControlToValidate="tbFromDate">
                                        <i class="fa fa-exclamation-triangle" title="Introduce una fecha válida" ></i>
                                </asp:RegularExpressionValidator>
                                <div class="input-group date">
                                    <asp:TextBox ID="tbFromDate" runat="server" Font-Names="Trebuchet MS" AutoComplete="off"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text" id="inp-sm">
                                            <i class="fa fa-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <asp:Label ID="lbDateRange2" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Fecha A: " AssociatedControlID="tbToDate"></asp:Label>
                                <asp:RegularExpressionValidator ID="reDate2" runat="server"
                                        Text="Error" Display="Dynamic"
                                        ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31/((0[13578])|(1[02])))|((29|30)/((0[1,3-9])|(1[0-2])))))/((20[0-9][0-9]))|((((0[1-9])|(1\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31/((0[13578])|(1[02])))|((29|30)/((0[1,3-9])|(1[0-2])))))/((19[0-9][0-9]))|(29/02/20(([02468][048])|([13579][26])))|(29/02/19(([02468][048])|([13579][26]))))$"
                                        ControlToValidate="tbToDate">
                                        <i class="fa fa-exclamation-triangle" title="Introduce una fecha válida" ></i>
                                </asp:RegularExpressionValidator>
                                <div class="input-group date">
                                    <asp:TextBox ID="tbToDate" runat="server" Font-Names="Trebuchet MS" AutoComplete="off"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text" id="Span1">
                                            <i class="fa fa-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <asp:Label ID="lbMcn" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Máquina: " AssociatedControlID="cbMachine"></asp:Label>
                                <asp:DropDownList ID="cbMachine" runat="server" Font-Names="Trebuchet MS" Font-Size="8pt" CssClass="form-control" ></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbColab" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Colaborador: " AssociatedControlID="cbColab"></asp:Label>
                                <asp:DropDownList ID="cbColab" runat="server" Font-Names="Trebuchet MS" Font-Size="8pt" CssClass="form-control"></asp:DropDownList>
                            </td>
                            <td>
                                 <asp:Label ID="lblExcel" Text="Exportar" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" ></asp:Label><br />
                                    &nbsp;&nbsp;&nbsp;<asp:ImageButton runat="server" ID="imgExcel" Height="22px" Width="26px" /> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="False" 
                                        Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" 
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Visible="True">
                                    <asp:ListItem Selected>Colapsar</asp:ListItem>
                                    <asp:ListItem>Expander</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Button ID="btSearch" runat="server" Text="Consultar" Font-Names="Trebuchet MS" Font-Size="10pt" CssClass="btn btn-primary" ></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table cellspacing="0" cellpadding="0" width="98.5%" border="0">
                    <tr >
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <C1WebGrid:C1WebGrid CssClass="gridBorder" ID="gvGridView" runat="server" GridLines="None"
                    CellSpacing="1" AutoGenerateColumns="False" BorderStyle="Solid"
                    BorderColor="DarkGray" Font-Size="9pt"
                    Font-Names="Trebuchet MS" GroupIndent="15px" AllowColMoving="True"
                    AllowSorting="True"
                    GroupByCaption="Arrastre aqui cualquier columna para agrupar"
                    CallbackWaitImageUrl="~/App_Themes/Theme1/ajaxspinner.gif" AllowAutoSize="True"
                    AllowColSizing="True" EnableTheming="True" BackColor="Gainsboro" 4124
                    ShowFooter="True" 
                    EmptyDataText="No hay información para mostrar">
                <Columns>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM_EMP_ADDR"  SortExpression="XXPRTM_EMP_ADDR" HeaderText="Soldador" AllowGroup="False" AllowMove="False">
                        <GroupInfo OutlineMode ="StartCollapsed">
                            <HeaderStyle CssClass="gridSubHeader1 " ></HeaderStyle>
                        </GroupInfo>
                        <%--<ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>--%>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM_EFF_DATE"  SortExpression="XXPRTM_EFF_DATE" HeaderText="Fecha" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Center" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM_REFERENCE"  SortExpression="XXPRTM_REFERENCE" HeaderText="Referencia" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Center" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="CL_PRODUCTO"  SortExpression="CL_PRODUCTO" HeaderText="Artículo" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Left" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="DE_SOLDADOR"  SortExpression="DE_SOLDADOR" HeaderText="Colaborador" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Left" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM_MACHINE"  SortExpression="XXPRTM_MACHINE" HeaderText="Máquina" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Center" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM__CHR01"  SortExpression="XXPRTM__CHR01" HeaderText="Min x Pieza" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM_START_TIME"  SortExpression="XXPRTM_START_TIME" HeaderText="Hora Inicio" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Center" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="XXPRTM_END_TIME"  SortExpression="XXPRTM_END_TIME" HeaderText="Hora Fin" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Center" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="PZ_META"  SortExpression="PZ_META" HeaderText="Pz Meta" AllowGroup="False" AllowMove="False" Aggregate="Sum" DataFormatString="{0:0}">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="CN_TRANSACCION"  SortExpression="CN_TRANSACCION" HeaderText="Pz Prod" AllowGroup="False" AllowMove="False" Aggregate="Sum" DataFormatString="{0:0}" >
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Center" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="MIN_TEORICA"  SortExpression="MIN_TEORICA" HeaderText="Min Prog" AllowGroup="False" AllowMove="False" Aggregate="Sum" >
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="DIF_DATE"  SortExpression="DIF_DATE" HeaderText="Tpo Total" AllowGroup="False" AllowMove="False" Aggregate="Sum" DataFormatString="{0:0}">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField=""  SortExpression="" HeaderText="Cumplimiento" AllowGroup="False" AllowMove="False">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField=""  SortExpression="" HeaderText="% Jornada" AllowGroup="False" AllowMove="False" DataFormatString="{0:P}" Visible="true" >
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField=""  SortExpression="" HeaderText="Ponderado" AllowGroup="False" AllowMove="False" Aggregate="Custom">
                        <GroupInfo>
                            <HeaderStyle CssClass="gridSubHeader2"></HeaderStyle>
                        </GroupInfo>
                        <ItemStyle HorizontalAlign="Right" CssClass="table1"></ItemStyle>
                    </C1WebGrid:C1BoundColumn>
                </Columns>
                <ItemStyle ForeColor="Black" CssClass="gridRow"></ItemStyle>
                <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6"></PagerStyle>
                <GroupingStyle Font-Size="8pt" Font-Names="Trebuchet MS" Font-Bold="True" CssClass="gridGroup"></GroupingStyle>
                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                <AlternatingItemStyle CssClass="gridAltRow" />
                <FooterStyle CssClass="gridFooter" />
            </C1WebGrid:C1WebGrid>
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lbMsgError" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="9pt"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="dsDataSource" runat="server" CancelSelectOnNullParameter="False" EnableCaching="True" CacheKeyDependency="c1gridcache"></asp:SqlDataSource>
            <uc2:myFooter ID="myFooter1" runat="server"  />
        </div>
        <mySidebar:mySidebar ID="mySidebar" runat="server" />
    </form>
    <script type="text/javascript">
        $('.input-group.date').datepicker({
            autoclose: true,
            language: "es"
        })/*.datepicker("setDate", new Date())*/;
        /*funcion para ocultar una columna de la tabla*/
        //rows = document.getElementById("gvGridView").rows;
        //for (i = 0; i < rows.length; i++) {
        //    rows[i].cells[13].style.display = "none";
        //}
    </script>
</body>
</html>