Imports System.Data.Odbc
Imports System.Data
Imports System.IO

Partial Class MT_MT02_16_13
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        If gvWelder.Rows.Count > 0 Then
            gvWelder.HeaderRow.TableSection = TableRowSection.TableHeader
            gvWelder.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        myGlobal.prGetUserInfo()
        myGlobal.prAllowView(1)
        btModUpdate.Visible = False
        Page.Header.DataBind()
        If tbFromDate.Text <> Nothing Then
            btModUpdate.Visible = True
        End If
        If Not Page.IsPostBack Then
            'Get_aboutWelder()
            fillSoldador()
            fillActivity()
            fillMch()
        End If



    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        Dim DateEffec As String = tbFromDate.Text
        If DateEffec <> Nothing Then
            Dim myStr As String = FillGridView()

            myConnection.prOpenOracleDB(Session("ent_index"))
            myConnection.cmOracle = New OdbcCommand(myStr, myConnection.dbConOracle)
            myConnection.drOracle = myConnection.cmOracle.ExecuteReader

            'gvWelder.DataSource = myConnection.drOracle
            'gvWelder.DataBind()

            Dim dt As DataTable = New DataTable()
            dt.Columns.AddRange(New DataColumn(8) {
                                New DataColumn("Id"),
                                New DataColumn("Soldador"),
                                New DataColumn("Actividad"),
                                New DataColumn("Maquina"),
                                New DataColumn("Referencia"),
                                New DataColumn("Hora_Inicio"),
                                New DataColumn("Hora_Fin"),
                                New DataColumn("XXPRTM_ACTIVITY"),
                                New DataColumn("PROGRESS_RECID")})
            While myConnection.drOracle.Read()
                dt.Rows.Add(myConnection.drOracle("XXPRTM_EMP_ADDR"),
                            myConnection.drOracle("DE_SOLDADOR"),
                            myConnection.drOracle("CODE_CMMT"),
                            myConnection.drOracle("XXPRTM_MACHINE"),
                            myConnection.drOracle("XXPRTM_REFERENCE"),
                            myConnection.drOracle("XXPRTM_START_TIME"),
                            myConnection.drOracle("XXPRTM_END_TIME"),
                            myConnection.drOracle("XXPRTM_ACTIVITY"),
                            myConnection.drOracle("PROGRESS_RECID"))
            End While
            myConnection.prCloseOracleDB()
            ViewState("dt") = dt
            Me.BindGridSoldador()

            btModUpdate.Visible = True

        Else
            'Label1.Text = "El campo Fecha es obligatorio"
            ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire({ icon:  'warning', title: 'Atencion!...', text:     'El campo Fecha es obligatorio',})</script>")
        End If
        'Response.Write(FillGridView())
    End Sub

    Protected Sub prSearch_Redirect()
        Dim DateEffec As String = tbFromDate.Text
        Dim myStr As String = FillGridView()

        myConnection.prOpenOracleDB(Session("ent_index"))
        myConnection.cmOracle = New OdbcCommand(myStr, myConnection.dbConOracle)
        myConnection.drOracle = myConnection.cmOracle.ExecuteReader


        Dim dt As DataTable = New DataTable()
        dt.Columns.AddRange(New DataColumn(8) {
                            New DataColumn("Id"),
                            New DataColumn("Soldador"),
                            New DataColumn("Actividad"),
                            New DataColumn("Maquina"),
                            New DataColumn("Referencia"),
                            New DataColumn("Hora_Inicio"),
                            New DataColumn("Hora_Fin"),
                            New DataColumn("XXPRTM_ACTIVITY"),
                            New DataColumn("PROGRESS_RECID")})
        While myConnection.drOracle.Read()
            dt.Rows.Add(myConnection.drOracle("XXPRTM_EMP_ADDR"),
                        myConnection.drOracle("DE_SOLDADOR"),
                        myConnection.drOracle("CODE_CMMT"),
                        myConnection.drOracle("XXPRTM_MACHINE"),
                        myConnection.drOracle("XXPRTM_REFERENCE"),
                        myConnection.drOracle("XXPRTM_START_TIME"),
                        myConnection.drOracle("XXPRTM_END_TIME"),
                        myConnection.drOracle("XXPRTM_ACTIVITY"),
                        myConnection.drOracle("PROGRESS_RECID"))
        End While
        myConnection.prCloseOracleDB()
        ViewState("dt") = dt
        Me.BindGridSoldador()


        'Response.Write(FillGridView())
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSave.Click
        Try
            Dim Colaborador As String
            Dim Activity As String
            Dim Machine As String
            Dim Reference As String
            Dim HourBeggin As String
            Dim HourEnd As String
            Dim Domain As String
            Dim UserCreate As String
            Dim DateEfective As String
            Dim myStr As String

            'almacenamos la informacion de las variable
            Colaborador = cbColab.SelectedValue
            Activity = cbActivity.SelectedValue
            'Machine = cbMachine.SelectedValue
            Machine = If(cbMachine.SelectedValue = " ", " ", cbMachine.SelectedValue)
            Reference = tbReference.Text
            HourBeggin = tbHoraInicio.Text 'Hora inicio
            HourEnd = tbHoraFin.Text 'Hora final
            UserCreate = Session("usr_shortalias")
            Domain = Session("ent_domain")
            DateEfective = tbFromDate.Text


            myStr = <sql>
                                INSERT INTO XXPRTM_HIST(
                                    XXPRTM_EFF_DATE,
                                    XXPRTM_EMP_ADDR,
                                    XXPRTM_REFERENCE,
                                    XXPRTM_ACTIVITY,
                                    XXPRTM_MACHINE,
                                    XXPRTM_START_TIME,
                                    XXPRTM_END_TIME,
                                    XXPRTM_CREATE_USR,
                                    XXPRTM_CREATE_DATE,
                                    XXPRTM_DOMAIN,
                                    PROGRESS_RECID
                                )VALUES(
                                    TO_DATE('<%= DateEfective %>','DD/MM/YYYY'),
                                    '<%= Colaborador %>',
                                    '<%= Reference %>',
                                    '<%= Activity %>',
                                    '<%= Machine %>' ,
                                    '<%= HourBeggin %>',
                                    '<%= HourEnd %>',
                                    '<%= UserCreate %>',
                                    TO_DATE(SYSDATE, 'DD/MM/YYYY'),
                                    '<%= Domain %>',
                                    TO_NUMBER(QAD.XXPRTM_HIST_SEQ.NEXTVAL))
                            </sql>

            myConnection.prExecuteSQL(myStr, 2)
            'Response.Write(myStr)
            ClientScript.RegisterStartupScript(Me.[GetType](), "Pop", "CloseModal();", True)
            ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire( 'Correcto!', 'Se ha registrado con exito', 'success' );</script>")
            prSearch_Redirect()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try


    End Sub

    Protected Sub btUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpdate.Click
        Dim Colaborador As String
        Dim Actividad As String
        Dim Maquina As String
        Dim Referencia As String
        Dim HoraInicio As String
        Dim HoraFin As String
        Dim Dominio As String
        Dim UsuarioCreado As String
        Dim FechaEfectiva As String
        Dim myStrSQL As String
        Dim ColabHiden As String
        Dim Progress As String

        Colaborador = cbColab.SelectedValue
        ColabHiden = hfWelderId.Value
        Actividad = cbActivity.SelectedValue
        'Maquina = cbMachine.SelectedValue
        Maquina = If(cbMachine.SelectedValue = " ", " ", cbMachine.SelectedValue)
        Referencia = tbReference.Text
        HoraInicio = tbHoraInicio.Text
        HoraFin = tbHoraFin.Text
        Dominio = Session("ent_domain")
        UsuarioCreado = Session("usr_shortalias")
        FechaEfectiva = tbFromDate.Text
        Progress = hfRecidProgress.Value

        myStrSQL = <sql>
                        UPDATE QAD.XXPRTM_HIST SET
                                    XXPRTM_REFERENCE = '<%= Referencia %>',
                                    XXPRTM_ACTIVITY = '<%= Actividad %>',
                                    XXPRTM_MACHINE = '<%= Maquina %>', 
                                    XXPRTM_START_TIME = '<%= HoraInicio %>',
                                    XXPRTM_END_TIME = '<%= HoraFin %>',
                                    XXPRTM_MODIF_USR = '<%= UsuarioCreado %>',
                                    XXPRTM_MODIF_DATE = TO_DATE(SYSDATE, 'DD/MM/YYYY')
                              WHERE XXPRTM_EFF_DATE = TO_DATE('<%= FechaEfectiva %>','DD/MM/YYYY') AND
                                    PROGRESS_RECID = <%= Progress %> AND
                                    XXPRTM_EMP_ADDR = '<%= ColabHiden %>' AND
                                    XXPRTM_DOMAIN = '<%= Dominio %>'
                                    
                   </sql>


        prInsertNewDatas(myStrSQL)
        fnClearForm()
        ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire( 'Correcto!', 'Su registro se ha actualizado', 'success' );</script>")
        prSearch_Redirect()
    End Sub

    Protected Sub prInsertNewDatas(query As String)
        myConnection.prExecuteSQL(query, 2)
    End Sub

    Function FillGridView() As String
        Dim myStr, b As String
        Dim DateEffec As String = tbFromDate.Text


        b = "SELECT XXPRTM_EMP_ADDR, DE_NOMBRE || ' ' || DE_APELLIDO_PATERNO || ' ' || DE_APELLIDO_MATERNO AS DE_SOLDADOR, XXPRTM_ACTIVITY, XXPRTM_MACHINE, CODE_CMMT, XXPRTM_REFERENCE, XXPRTM_START_TIME, XXPRTM_END_TIME, XXPRTM_HIST.PROGRESS_RECID " &
            " FROM QAD.XXPRTM_HIST" &
                " LEFT JOIN qad.AD_M_EM_##_QAD ON upper(XXPRTM_EMP_ADDR) = CL_EMP" &
                " LEFT JOIN QAD.CODE_MSTR ON upper(XXPRTM_ACTIVITY) = CODE_VALUE and UPPER(CODE_FLDNAME) = 'XXPRTM_ACTIVITY' " &
                "  and CODE_DOMAIN = '" & Session("ent_domain") & "' " &
                " WHERE XXPRTM_DOMAIN = '" & Session("ent_domain") & "' AND XXPRTM_EFF_DATE = to_date('" & DateEffec & "','DD/MM/YYYY') AND XXPRTM_ACTIVITY <> ' ' AND XXPRTM_ACTIVITY <> 'AC0'"

        myStr = b


        'If Session("usr_alias") = "luis.lara" Or Session("usr_alias") = "enrique.nunez" Or Session("usr_alias") = "itzel.carrillo" Then
        '    Response.Write("consulta:" & myStr & "<br>")
        '    'myStr = ""
        'End If


        FillGridView = myStr
    End Function

    Protected Sub fillSoldador()
        Dim myStrSQL As String
        Dim domain As String
        domain = Session("ent_domain")
        myStrSQL = <sql>
                        select UPPER (CL_EMP) as CL_SOLDADOR , DE_NOMBRE || ' ' || DE_APELLIDO_PATERNO || ' ' || DE_APELLIDO_MATERNO AS DE_SOLDADOR 
                        from qad.ad_m_em_##_QAD where upper(cl_puesto) IN ('SOR','SOC') and upper(cl_status) = 'AC' and upper(CL_ENTIDAD) = 'SCM' and CL_DEPTO = 'CSOL' ORDER BY DE_APELLIDO_PATERNO ASC
                   </sql>
        myConnection.prOpenOracleDB(Session("ent_index"))
        myConnection.cmOracle = New OdbcCommand(myStrSQL, myConnection.dbConOracle)
        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
        cbColab.Items.Add(New ListItem("Selecciona una opcion", ""))
        While myConnection.drOracle.Read()
            cbColab.Items.Add(New ListItem(myConnection.drOracle("CL_SOLDADOR") + "-" + myConnection.drOracle("DE_SOLDADOR"), myConnection.drOracle("CL_SOLDADOR")))
        End While
        myConnection.dbConOracle.Close()
    End Sub

    Protected Sub fillActivity()
        Dim myStrSQL As String

        myStrSQL = "select CODE_VALUE, CODE_CMMT from QAD.CODE_MSTR where UPPER(CODE_FLDNAME) = 'XXPRTM_ACTIVITY' AND UPPER(CODE_DOMAIN) =  '" & Session("ent_domain") & "' and CODE_VALUE <> 'AC0'"


        myConnection.prOpenOracleDB(Session("ent_index"))
        myConnection.cmOracle = New OdbcCommand(myStrSQL, myConnection.dbConOracle)
        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
        cbActivity.Items.Add(New ListItem("Selecciona una opcion", ""))
        While myConnection.drOracle.Read()
            cbActivity.Items.Add(New ListItem(myConnection.drOracle("CODE_CMMT"), myConnection.drOracle("CODE_VALUE")))
        End While
        myConnection.dbConOracle.Close()
    End Sub

    Protected Sub fillMch()
        Dim myStr As String
        Dim dynamicMch As String
        myStr = "SELECT CODE_CMMT FROM QAD.CODE_MSTR " &
                        "WHERE UPPER(CODE_FLDNAME) = 'XXWC_WKCTR' AND " &
                            "UPPER(CODE_VALUE) = 'MCH' AND " &
                            "UPPER(CODE_DOMAIN) = '" & Session("ent_domain") & "' "

        myConnection.prOpenOracleDB(Session("ent_index"))
        myConnection.cmOracle = New OdbcCommand(myStr, myConnection.dbConOracle)
        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
        While myConnection.drOracle.Read()
            dynamicMch = myConnection.drOracle("CODE_CMMT")
        End While
        myConnection.prCloseOracleDB()

        myStr =
                    "select wc_mch, wc_desc from qad.wc_mstr " &
                    "where upper(wc_domain) = '" & Session("ent_domain") & "' AND UPPER(WC__CHR03) = 'SCM' AND UPPER(wc_wkctr) IN (" & dynamicMch & ") " &
                    "order by wc_desc desc"

        myConnection.prOpenOracleDB(Session("ent_index"))
        myConnection.cmOracle = New OdbcCommand(myStr, myConnection.dbConOracle)
        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
        cbMachine.Items.Add(New ListItem("Seleccionar una opción", " "))
        While myConnection.drOracle.Read()
            cbMachine.Items.Add(New ListItem(myConnection.drOracle("wc_mch"), myConnection.drOracle("wc_mch")))
        End While
        myConnection.prCloseOracleDB()
        'Response.Write(myStr)
    End Sub

    Protected Sub btModalUpdate_Click(ByVal sender As Object, ByVal e As EventArgs)
        cbColab.Enabled = False
        btSave.Visible = False
        btUpdate.Visible = True
        Dim date2 As Date = Now
        Dim date1 As Date = tbFromDate.Text
        Dim row As GridViewRow = (TryCast((TryCast(sender, Button)).NamingContainer, GridViewRow))

        Dim days As Long = DateDiff(DateInterval.Day, date1, date2)

        If days > 10 Then
            btUpdate.Visible = False
            cbActivity.Enabled = False
            cbMachine.Enabled = False
            tbReference.Enabled = False
            tbHoraInicio.Enabled = False
            tbHoraFin.Enabled = False
        End If

        hfWelderId.Value = (TryCast(row.FindControl("hfId"), HiddenField)).Value
        hfActivity.Value = (TryCast(row.FindControl("hfAct"), HiddenField)).Value
        hfRecidProgress.Value = (TryCast(row.FindControl("hfRecid"), HiddenField)).Value
        cbColab.SelectedValue = (TryCast(row.FindControl("hfId"), HiddenField)).Value
        cbActivity.SelectedValue = (TryCast(row.FindControl("hfAct"), HiddenField)).Value
        'cbActivity.SelectedIndex = cbActivity.Items.IndexOf(cbActivity.Items.FindByText((TryCast(row.FindControl("lbActivity"), Label)).Text))
        cbMachine.SelectedValue = (TryCast(row.FindControl("lbMaquina"), Label)).Text
        tbReference.Text = (TryCast(row.FindControl("lbReference"), Label)).Text
        tbHoraInicio.Text = (TryCast(row.FindControl("lbHourBegin"), Label)).Text
        tbHoraFin.Text = (TryCast(row.FindControl("lbHourEnd"), Label)).Text
        ClientScript.RegisterStartupScript(Me.[GetType](), "Pop", "OpenModalUpdate();", True)
    End Sub


    Protected Sub btModUpdate_Click(ByVal sender As Object, ByVal e As EventArgs)
        cbColab.Enabled = True
        cbActivity.Enabled = True
        cbMachine.Enabled = True
        tbReference.Enabled = True
        tbHoraInicio.Enabled = True
        tbHoraFin.Enabled = True
        cbColab.SelectedValue = ""
        cbActivity.SelectedValue = ""
        tbReference.Text = ""
        tbHoraInicio.Text = ""
        tbHoraFin.Text = ""
        cbMachine.SelectedValue = " "
        btSave.Visible = True
        btUpdate.Visible = False
        ClientScript.RegisterStartupScript(Me.[GetType](), "Pop", "OpenModalUpdate();", True)
    End Sub

    Protected Sub BindGridSoldador()
        gvWelder.DataSource = TryCast(ViewState("dt"), DataTable)
        gvWelder.DataBind()
    End Sub

    Protected Function fnClearForm()
        cbColab.SelectedValue = ""
        cbActivity.SelectedValue = ""
        tbReference.Text = ""
        tbHoraInicio.Text = ""
        tbHoraFin.Text = ""
        cbMachine.SelectedValue = " "
        hfWelderId.Value = " "
        hfActivity.Value = " "
        hfRecidProgress.Value = " "
    End Function




End Class
