 <%@ Page Language="VB" AutoEventWireup="false" CodeFile="MT02.16-12.aspx.vb" Inherits="MT_MT02_16_12" culture="es-MX" uiCulture="es-MX"  %>

<%@ Register Src="~/_private/myGlobal.ascx" TagPrefix="uc1" TagName="myGlobal" %>
<%@ Register Src="~/_private/myConnection.ascx" TagPrefix="uc1" TagName="myConnection" %>
<%@ Register Src="~/_private/myCommon.ascx" TagPrefix="uc1" TagName="myCommon" %>
<%@ Register Src="~/UT/myHeader.ascx" TagPrefix="uc1" TagName="myHeader" %>
<%@ Register Src="~/UT/myFooter.ascx" TagPrefix="uc1" TagName="myFooter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title><%#myGlobal.GetTitle(2)%></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link href="../../../assets/css/<%# myCommon.GetPageStyle(Session("ent_index"))%>/bootstrap.custom.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.11.4/datatables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/2.2.2/css/buttons.dataTables.min.css" />
    <link href="../../../assets/plugins/bootstrap-datepicker-1.9.0-dist/css/bootstrap-datepicker.standalone.min.css" rel="stylesheet" />

    
</head>


<body class="d-flex flex-column min-vh-100">
    <form id="form1" runat="server">
        <div>
            <uc1:myHeader runat="server" ID="myHeader" />
            <uc1:myConnection runat="server" ID="myConnection" />
            <uc1:myCommon runat="server" ID="myCommon" />
            <uc1:myGlobal runat="server" ID="myGlobal" />
            <asp:ScriptManager EnablePageMethods="true" ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
        </div>

         <div class="container-fluid">
            <div class="col">
            <div class="row mb-3">
                    <asp:Panel ID="pnMain" runat="server" IButtonControl="btSearch" CssClass="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="Label3" runat="server" Text="Fecha: " AssociatedControlID="tbFromDate"></asp:Label>
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="tbFromDate" AutoComplete="off" runat="server" CssClass="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm"></asp:TextBox>
                                         <asp:CalendarExtender ID="tbDate1_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbFromDate" />
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="spFromDate"><i class="far fa-calendar-alt"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lbMcn" runat="server" Text="Máquina: " AssociatedControlID="cbMachine"></asp:Label>
                                    <asp:DropDownList ID="cbMachine" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mt-4">
                                  <div class="col d-flex align-self-end ">
                                        <asp:Button ID="btSearch" runat="server" Text="Consultar" CssClass="btn btn-primary btn-sm"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                       </asp:Panel>
                      </div>
                                
                 </div>

                <td colspan="6" style="  text-align: center; height: 20px; background-color:Transparent; ">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Font-Names="Trebuchet MS" Font-Size="10pt"></asp:Label>
                </td>

                 <asp:Panel ID="pnGrid" runat="server" CssClass="row">
                    
                        <div class="card">
                            <div class="card-body">
                                <asp:GridView ID="gvTransactions" Width="100%" CssClass="table table-sm table-bordered table-striped table-responsive-sm display nowrap" AutoGenerateColumns="False" runat="server" ShowFooter="true" EmptyDataText="No hay información para mostrar" Font-Size="8pt" >
                                    <Columns>
                                        <asp:BoundField HeaderText="Id" DataField="CL_SOLDADOR" />
                                        <asp:BoundField HeaderText="Soldador" DataField="DE_SOLDADOR" />
                                        <asp:BoundField HeaderText="Producto" DataField="CL_PRODUCTO" />
                                        <asp:BoundField HeaderText="Referencia" DataField="NU_OV_OT" />
                                        <asp:BoundField HeaderText="Pz" DataField="CN_TRANSACCION" />
                                         <asp:BoundField HeaderText="Tiempo Pz" DataField="TIEMPO_PZ" />
                                         <asp:BoundField HeaderText="MEAL"  DataField="XXPRTM_MEAL" /> 
                                             
                                         <asp:BoundField HeaderText="SPLICE" DataField="XXPRTM_SPLICE" />
                                         <asp:BoundField HeaderText="START_END" DataField="XXPRTM_START_END" />
                                        <asp:TemplateField HeaderText="Comida" >
                                           <ItemTemplate>
                                               <asp:CheckBox ID="CkMeal" runat="server" CssClass="form-check" />
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Empalme" >
                                           <ItemTemplate>
                                               <asp:CheckBox ID="CkSplice" runat="server" CssClass="form-check"  />
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arranque/Fin" >
                                           <ItemTemplate>
                                               <asp:CheckBox ID="CkStartEnd" runat="server" CssClass="form-check" />
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Inicio" >
                                           <ItemTemplate>
                                               <asp:TextBox ID="tbHourBegin" CssClass="form-control" runat="server" Text='<%# Bind("XXPRTM_START_TIME")%>' TextMode="Time" format="HH:mm"></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Fin" >
                                           <ItemTemplate>
                                               <asp:TextBox ID="tbHourEnd" CssClass="form-control" runat="server" Text='<%# Bind("XXPRTM_END_TIME") %>' TextMode="Time" format="HH:mm"></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table-primary" />
                                    <FooterStyle CssClass="table-primary fw-bold" />
                                </asp:GridView>
                            </div>
                        </div>
                    
               

                </asp:Panel>

                <div class="row">
                    <div class="col">
                        <asp:Button ID="btChange" runat="server" Text="Modificar" CssClass="btn btn-primary btn-sm"></asp:Button>
                    </div>
                </div>

              </div>
   
             <script src="../../../assets/js/jquery.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
        <script src="https://unpkg.com/@popperjs/core@2" crossorigin="anonymous"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.11.4/datatables.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.2/js/dataTables.buttons.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.2/js/buttons.html5.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.2/js/buttons.print.min.js"></script>
        <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <script src="../../../assets/plugins/bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js" crossorigin="anonymous"></script>
        <script src="../../../assets/plugins/bootstrap-datepicker-1.9.0-dist/locales/bootstrap-datepicker.es.min.js" crossorigin="anonymous"></script>

        <%-- Time picker --%>
        <script src="../../../assets/js/jquery-clock-timepicker.min.js"></script>

         <script language="javascript" type="text/javascript">
             document.getElementById('tbFromDate').readOnly = true;

             var resume_table = document.getElementById("gvTransactions");

             for (var i = 0, row; row = resume_table.rows[i]; i++) {

                 for (var j = 0, col; col = row.cells[j]; j++) {

                     console.log('Txt: ${col.innerText} \tFila: ${i} \t Celda: ${j}');
                 }
             }


             //col_num = document.getElementById("column_numbder").value;
             rows = document.getElementById("gvTransactions").rows;
             for (i = 0; i < rows.length; i++) {
                 rows[i].cells[6].style.display = "none";
                 rows[i].cells[7].style.display = "none";
                 rows[i].cells[8].style.display = "none";


             }



             $(document).ready(function () {

                 $("#tbHourBegin").clockTimePicker({
                     precision: 30,
                     minimum: "07:00",
                     maximum: "20:00"
                 });
             });



             function fnResubmit(val, type) {
                 fnClearFields();
                 if (type == 1) {
                     document.getElementById("tbOrder").value = val;
                 }
                 else {
                     document.getElementById("tbComments").value = val;
                 }
                 document.forms[0].btSearch.click();
             }

             function fnClearFields() {
                 document.getElementById("tbPart").value = '';
                 document.getElementById("tbOrder").value = '';
                 document.getElementById("tbComments").value = '';
             }

             function OnChange(dropdown) {
                 var myindex = dropdown.selectedIndex;
                 var SelValue = dropdown.options[myindex].value;
                 fnHidde(SelValue)
             }



             function tableTransactions() {
                 $('#gvTransactions').DataTable({
                     "scrollX": true,
                     "paging": true,
                     language: {
                         "decimal": "",
                         "emptyTable": "No hay información",
                         "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                         "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                         "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                         "infoPostFix": "",
                         "thousands": ",",
                         "lengthMenu": "Mostrar _MENU_ Entradas",
                         "loadingRecords": "Cargando...",
                         "processing": "Procesando...",
                         "search": "Buscar:",
                         "zeroRecords": "Sin resultados encontrados",
                         "paginate": {
                             "first": "Primero",
                             "last": "Ultimo",
                             "next": "Siguiente",
                             "previous": "Anterior"
                         }
                     },

                 });
             }



             $(document).ready(tableTransactions());


         </script>
    </form>
     <uc1:myFooter runat="server" ID="myFooter" />
</body>
</html>

