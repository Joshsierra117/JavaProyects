<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MT02.16-13.aspx.vb" Inherits="MT_MT02_16_13" culture="es-MX" uiCulture="es-MX" %>

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
                              
                            <div class="col-md-4 mt-4">
                              <div class="col d-flex align-self-end ">
                                    <asp:Button ID="btSearch" runat="server" Text="Consultar" CssClass="btn btn-primary btn-sm"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-4 mt-4">
                               <div class="col d-flex align-self-end ">
                                   <asp:Button id="btModUpdate" OnClick="btModUpdate_Click" runat="server" Text="Nuevo Registro" CssClass="btn btn-primary btn-sm"  />
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
                                <asp:GridView ID="gvWelder" CssClass="table table-sm table-bordered table-striped table-responsive-sm display nowrap" AutoGenerateColumns="False" runat="server" ShowFooter="true" EmptyDataText="No hay información para mostrar" Font-Size="12pt" >
                                    <Columns>   
                                        <asp:TemplateField Visible="true">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hfId"  Value='<%# Bind("Id") %>' />
                                                <asp:HiddenField runat="server" ID="hfAct"  Value='<%# Bind("XXPRTM_ACTIVITY")%>' />
                                                <asp:HiddenField runat="server" ID="hfRecid"  Value='<%# Bind("PROGRESS_RECID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Soldador">
                                            <ItemTemplate>
                                                <asp:Label ID="lbWelder" runat="server" Text='<%# Bind("Soldador")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actividad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbActividad" runat="server" Text='<%# Bind("Actividad")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Máquina">
                                            <ItemTemplate>
                                                <asp:Label ID="lbMaquina" runat="server" Text='<%# Bind("Maquina")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Referencia">
                                            <ItemTemplate>
                                                <asp:Label ID="lbReference" runat="server" Text='<%# Bind("Referencia")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Inicio">
                                            <ItemTemplate>
                                                <asp:Label ID="lbHourBegin" runat="server" Text='<%# Bind("Hora_Inicio")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Fin">
                                            <ItemTemplate>
                                                <asp:Label ID="lbHourEnd" runat="server" Text='<%# Bind("Hora_Fin")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Opciones" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Button id="btModalUpdate" OnClick="btModalUpdate_Click" runat="server" Text="editar" CssClass="btn btn-primary btn-xs" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table-primary" />
                                    <FooterStyle CssClass="table-primary fw-bold" />
                                </asp:GridView>
                            </div>
                        </div>
                    
               

                </asp:Panel>

                 <!-- Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog modal-lg">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Registro</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                   <div class="container-fluid"> 
                    <div class="row">
                            <div class ="col-6">
                               <asp:Label ID="lbColab" runat="server" Text="Colaborador" AssociatedControlID="cbColab"></asp:Label>
                                <asp:DropDownList ID="cbColab" CssClass="form-select" runat="server"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hfWelderId" Value='' />
                                <asp:HiddenField runat="server" ID="hfActivity" Value='' />
                                <asp:HiddenField runat="server" ID="hfRecidProgress" Value='' />
                                <asp:RequiredFieldValidator ID="rfColab" runat="server" Display="Dynamic" ControlToValidate="cbColab" ValidationGroup="vgSave">
                                                    <i class="bi bi-x-circle-fill" title="Este campo es obligatorio"></i>
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class ="col-6">
                               <asp:Label ID="lbActivity" runat="server" Text="Actividad" AssociatedControlID="cbActivity"></asp:Label>
                                <asp:DropDownList ID="cbActivity" CssClass="form-select" runat="server" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfActivity" runat="server" Display="Dynamic" ControlToValidate="cbActivity" ValidationGroup="vgSave">
                                                    <i class="bi bi-x-circle-fill" title="Este campo es obligatorio"></i>
                                </asp:RequiredFieldValidator>
                            </div>
                       </div>
                       <div class="row">
                           <div class ="col-md-6">
                                <asp:Label ID="lbReference" runat="server" Text="Referencia:" AssociatedControlID="tbReference"></asp:Label>
                                <asp:TextBox ID="tbReference" runat="server" MaxLength="18" CssClass="form-control form-control-sm"></asp:TextBox>
                                
                            </div>
                           <div class ="col-md-6">
                                <asp:Label ID="lbHoraInicio" runat="server" TextMode="Time" format="HH:mm" Text="Hora Inicio:" AssociatedControlID="tbHoraInicio"></asp:Label>
                                <asp:TextBox ID="tbHoraInicio" runat="server" CssClass="form-control form-control-sm" TextMode="Time" format="HH:mm"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="rfHoraInicio" runat="server" Display="Dynamic" ControlToValidate="tbHoraInicio" ValidationGroup="vgSave">
                                                    <i class="bi bi-x-circle-fill" title="Este campo es obligatorio"></i>
                                </asp:RequiredFieldValidator>
                            </div>
                       </div>
                       <div class="row">
                           <div class ="col-md-6">
                                <asp:Label ID="lbHoraFin" runat="server" Text="Hora Fin:" AssociatedControlID="tbHoraFin"></asp:Label>
                                <asp:TextBox ID="tbHoraFin" runat="server" CssClass="form-control form-control-sm" TextMode="Time" format="HH:mm"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="rfHoraFin" runat="server" Display="Dynamic" ControlToValidate="tbHoraFin" ValidationGroup="vgSave">
                                                    <i class="bi bi-x-circle-fill" title="Este campo es obligatorio"></i>
                                </asp:RequiredFieldValidator>
                            </div>
                           <div class="col-md-6">
                                    <asp:Label ID="lbMcn" runat="server" Text="Máquina: " AssociatedControlID="cbMachine"></asp:Label>
                                    <asp:DropDownList ID="cbMachine" CssClass="form-select" runat="server"></asp:DropDownList>
                           </div>
                       </div>
                   </div>
                  </div>
                  <div class="modal-footer">
                      <div class="messagealert mr-auto" id="alert_container" style="float: left;"></div>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btSave" runat="server" Text="Guardar" CssClass="btn btn-primary" UseSubmitBehavior="false" ValidationGroup="vgSave"></asp:Button>
                    <asp:Button ID="btUpdate" runat="server" Text="Modificar" CssClass="btn btn-primary"></asp:Button>
                  </div>
                </div>
              </div>
            </div>

              </div>
   
             <%--<script src="../../../assets/js/jquery.min.js"></script>--%>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
        <script src="https://unpkg.com/@popperjs/core@2" crossorigin="anonymous"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.11.4/datatables.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.2/js/dataTables.buttons.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.2/js/buttons.html5.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.2/js/buttons.print.min.js"></script>

        <script src="../../../assets/plugins/bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js" crossorigin="anonymous"></script>
        <script src="../../../assets/plugins/bootstrap-datepicker-1.9.0-dist/locales/bootstrap-datepicker.es.min.js" crossorigin="anonymous"></script>
        <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <%-- Time picker --%>
        <script src="../../../assets/js/jquery-clock-timepicker.min.js"></script>

         <script language="javascript" type="text/javascript">

             document.getElementById('tbFromDate').readOnly = true;


             function tableTransactions() {
                 $('#gvWelder').DataTable({
                     "scrollX": false,
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

             rows = document.getElementById("gvWelder").rows;
             for (i = 0; i < rows.length; i++) {
                 rows[i].cells[0].style.display = "none";

             }


             $(document).ready(tableTransactions());

             $('#btSave').on('click', function (e) {


                 if (!$("#cbColab").val()) {
                     setTimeout(function () { $('#cbColab').focus(); }, 500);
                     ShowMessage('Selecciona un colaborador', "1");
                     e.preventDefault();
                 }

                 else if (!$("#cbActivity").val()) {
                     setTimeout(function () { $('#cbActivity').focus(); }, 500);
                     ShowMessage('Selecciona una actividad', "1");
                     e.preventDefault();
                 }
                 else if (!$("#tbHoraInicio").val()) {
                     setTimeout(function () { $('#tbHoraInicio').focus(); }, 500);
                     ShowMessage('Selecciona una hora de inicio', "1");
                     e.preventDefault();
                 }
                 else if (!$("#tbHoraFin").val()) {
                     setTimeout(function () { $('#tbHoraFin').focus(); }, 500);
                     ShowMessage('Selecciona una hora de Final', "1");
                     e.preventDefault();
                 }



             });


             $('#btUpdate').on('click', function (e) {


                 if (!$("#cbColab").val()) {
                     setTimeout(function () { $('#cbColab').focus(); }, 500);
                     ShowMessage('Selecciona un colaborador', "1");
                     e.preventDefault();
                 }

                 else if (!$("#cbActivity").val()) {
                     setTimeout(function () { $('#cbActivity').focus(); }, 500);
                     ShowMessage('Selecciona una actividad', "1");
                     e.preventDefault();
                 }
                 else if (!$("#tbHoraInicio").val()) {
                     setTimeout(function () { $('#tbHoraInicio').focus(); }, 500);
                     ShowMessage('Selecciona una hora de inicio', "1");
                     e.preventDefault();
                 }
                 else if (!$("#tbHoraFin").val()) {
                     setTimeout(function () { $('#tbHoraFin').focus(); }, 500);
                     ShowMessage('Selecciona una hora de Final', "1");
                     e.preventDefault();
                 }



             });

             $('#btModUpdate').on('click', function (e) {

                 $("#exampleModal textarea").val("");
                 $("#exampleModal select").val("");
                 $("#exampleModal input[type='checkbox']").prop('checked', false).change();
                 document.getElementById("cbColab").value = "";
                 document.getElementById("hfWelderId").value = "";
                 document.getElementById("hfActivity").value = "";
                 document.getElementById("tbReference").value = "";
                 document.getElementById("tbHoraInicio").value = "";
                 document.getElementById("tbHoraFin").value = "";



             });

             var exampleModal = document.getElementById('exampleModal')
             exampleModal.addEventListener('show.bs.modal', function (event) {
                 var button = event.relatedTarget
                 var recipient = button.getAttribute('data-bs-whatever')
                 var modalTitle = exampleModal.querySelector('.modal-title')
                 var modalBodyInput = exampleModal.querySelector('.modal-body input')

                 modalBodyInput.value = recipient
             })

             $('#exampleModal').on('hide.bs.modal', function (event) {
                 $("#exampleModal textarea").val("");
                 $("#exampleModal select").val("");
                 $("#exampleModal input[type='checkbox']").prop('checked', false).change();
                 document.getElementById("hfWelderId").value = "";
                 document.getElementById("hfActivity").value = "";
                 document.getElementById("tbReference").value = "";
                 document.getElementById("tbHoraInicio").value = "";
                 document.getElementById("tbHoraFin").value = "";
             });
             //var btNewWelder = document.getElementById('btNewRegis');
             //btNewWelder.addEventListener('click', function () {
             //    // alert("hola mundo");
             //    document.getElementById('btUpdate').style.visibility = "hidden";
             //    document.getElementById('btSave').style.visibility = "visible";
             //});


             function CloseModal() {
                 var myModal = new bootstrap.Modal(document.getElementById("exampleModal"), {});
                 //$('[id*=myModal]').show;
                 myModal.hide();
             }

             function OpenModalUpdate() {
                 var myModal = new bootstrap.Modal(document.getElementById("exampleModal"), {});
                 //$('[id*=myModal]').show;
                 myModal.show();

             }

             function ShowMessage(message, messagetype) {
                 var cssclass;
                 switch (messagetype) {
                     case 'mySuccess', '0':
                         cssclass = 'alert-success'
                         break;
                     case 'myError', '1':
                         cssclass = 'alert-danger'
                         break;
                     case 'myWarning', '3':
                         cssclass = 'alert-warning'
                         break;
                     default:
                         cssclass = 'alert-info'
                 }

                 $('#alert_container').append('<div class="alert ' + cssclass + '">' + message + '</div>')
                 setTimeout(function () {
                     $("#alert_container").children('.alert:first-child').remove();
                 }, 4500);

             }

         </script>
    </form>
     <uc1:myFooter runat="server" ID="myFooter" />
</body>
</html>
