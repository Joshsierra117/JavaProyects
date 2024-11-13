Imports System.Data.Odbc
Imports System.Data
Imports System.IO

Partial Class MT_MT02_16_12
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        If gvTransactions.Rows.Count > 0 Then
            gvTransactions.HeaderRow.TableSection = TableRowSection.TableHeader
            gvTransactions.FooterRow.TableSection = TableRowSection.TableFooter
        End If


    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        myGlobal.prGetUserInfo()
        myGlobal.prAllowView(1)

        Page.Header.DataBind()
        If tbFromDate.Text <> "" And cbMachine.SelectedValue <> "" Then
            btChange.Visible = True
        Else
            btChange.Visible = False
        End If

        If Not Page.IsPostBack Then
            'Get_aboutWelder()
            fillMch()
        End If

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransactions.RowDataBound
        Dim myMeal As Integer = myCommon.fnGetColumnByName(gvTransactions, "MEAL")
        Dim mySplice As Integer = myCommon.fnGetColumnByName(gvTransactions, "SPLICE")
        Dim myStartEnd As Integer = myCommon.fnGetColumnByName(gvTransactions, "START_END")
        If gvTransactions.Rows.Count >= 0 Then
            Dim myFood As String = e.Row.Cells(myMeal).Text
            Dim myEmpalme As String = e.Row.Cells(mySplice).Text
            Dim myInicioFin As String = e.Row.Cells(myStartEnd).Text
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim Checkbox1 As CheckBox = TryCast(e.Row.FindControl("CkMeal"), CheckBox)
                Dim Checkbox2 As CheckBox = TryCast(e.Row.FindControl("CkSplice"), CheckBox)
                Dim Checkbox3 As CheckBox = TryCast(e.Row.FindControl("CkStartEnd"), CheckBox)

                If myFood <> "" And myFood <> "0" And myFood <> 0 And myFood <> "MEAL" Then
                    Checkbox1.Checked = True
                End If
                If myEmpalme <> "" And myEmpalme <> "0" And myEmpalme <> 0 And myEmpalme <> "SPLICE" Then
                    Checkbox2.Checked = True
                End If
                If myInicioFin <> "" And myInicioFin <> "0" And myInicioFin <> 0 And myInicioFin <> "START_END" Then
                    Checkbox3.Checked = True
                End If

            End If
        End If



    End Sub

    Protected Sub gvTransactions_PreRender(sender As Object, e As EventArgs) Handles gvTransactions.PreRender

        If gvTransactions.Rows.Count > 0 Then
            For i = 0 To gvTransactions.Rows.Count - 1
                Dim date2 As Date = Now
                Dim date1 As Date = tbFromDate.Text


                Dim days As Long = DateDiff(DateInterval.Day, date1, date2)

                If days > 10 Then

                    btChange.Visible = False
                    TryCast(gvTransactions.Rows(i).FindControl("tbHourBegin"), TextBox).Enabled = False
                    TryCast(gvTransactions.Rows(i).FindControl("tbHourEnd"), TextBox).Enabled = False
                    TryCast(gvTransactions.Rows(i).FindControl("CkMeal"), CheckBox).Enabled = False
                    TryCast(gvTransactions.Rows(i).FindControl("CkSplice"), CheckBox).Enabled = False
                    TryCast(gvTransactions.Rows(i).FindControl("CkStartEnd"), CheckBox).Enabled = False
                End If
            Next
        End If

    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        If tbFromDate.Text <> Nothing Then
            If cbMachine.SelectedValue <> " " Then

                Dim myStr As String = FillGridView()

                myConnection.prOpenOracleDB(Session("ent_index"))
                myConnection.cmOracle = New OdbcCommand(myStr, myConnection.dbConOracle)
                myConnection.drOracle = myConnection.cmOracle.ExecuteReader

                gvTransactions.DataSource = myConnection.drOracle
                gvTransactions.DataBind()

                myConnection.prCloseOracleDB()

            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire({ icon:  'warning', title: 'Atencion!...', text:  'El campo Máquina es obligatorio',})</script>")
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire({ icon:  'warning', title: 'Atención!...', text:  'El campo Fecha es obligatorio',})</script>")
        End If

        'Response.Write(FillGridView())
    End Sub

    Function FillGridView() As String
        Dim myStr, b As String
        Dim myEffDate As String = tbFromDate.Text
        Dim myMch As String = cbMachine.SelectedValue


        b = <sql>
                SELECT UPPER(NU_FACTURA)AS CL_SOLDADOR, 
                    DE_NOMBRE || ' ' || DE_APELLIDO_PATERNO || ' ' || DE_APELLIDO_MATERNO AS DE_SOLDADOR , 
                    NU_OV_OT , 
                    CL_PRODUCTO,  
                    SUM( CN_TRANSACCION) AS CN_TRANSACCION,
                    Round((TIEMPO_PZ/60), 1) as TIEMPO_PZ,
                    CASE WHEN XXPRTM_MEAL IS NULL THEN 0 ELSE XXPRTM_MEAL END XXPRTM_MEAL,
                    CASE WHEN XXPRTM_SPLICE IS NULL THEN 0 ELSE XXPRTM_SPLICE END XXPRTM_SPLICE,
                    CASE WHEN XXPRTM_START_END IS NULL THEN 0 ELSE XXPRTM_START_END END XXPRTM_START_END,
                    XXPRTM_START_TIME,
                    XXPRTM_END_TIME
                FROM QAD.IC_D_WO_##_A_QAD
                LEFT JOIN qad.AD_M_EM_##_QAD ON upper(NU_FACTURA) = CL_EMP
                LEFT JOIN qad.XXPRTM_HIST ON UPPER (NU_FACTURA) = XXPRTM_EMP_ADDR AND 
                                                     NU_OV_OT = XXPRTM_REFERENCE AND
                                                     FE_TRANSACCION = XXPRTM_EFF_DATE AND 
                                                     CL_MAQUINA = UPPER(XXPRTM_MACHINE) 
                WHERE FE_TRANSACCION = to_date('<%= myEffDate %>', 'DD/MM/YYYY') AND CL_TIPO_TRANSACC = 'RCT-WO' AND CL_MAQUINA IN ('<%= myMch %>') /* AND CL_DEPTO IN ('PRO', 'LOG') */
                    /* AND CL_STATUS = 'AC'  AND CL_ENTIDAD = 'SCM'*/
                GROUP BY  UPPER(NU_FACTURA) , NU_OV_OT, CL_PRODUCTO, DE_NOMBRE,DE_APELLIDO_PATERNO, DE_APELLIDO_MATERNO, TIEMPO_PZ, XXPRTM_MEAL, XXPRTM_SPLICE, XXPRTM_START_END, XXPRTM_START_TIME, XXPRTM_END_TIME 
                ORDER BY UPPER(NU_FACTURA)
            </sql>

        myStr = b
        FillGridView = myStr

        ' Response.Write(myStr)
    End Function

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
        ' Response.Write(myStr)
    End Sub

    Protected Sub btChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btChange.Click
        Try
            Dim Swal As String
            Dim clave As String
            Dim TiempoPz As String
            Dim soldador As String
            Dim producto As String
            Dim referencia As String
            Dim pieza As String
            Dim Domain As String
            Dim UserCreate As String
            Dim DateEfective As String
            Dim Machine As String
            Dim HourBeggin As String
            Dim HourEnd As String
            Dim Meal As Boolean = False
            Dim splice As Boolean = False
            Dim StartEnd As Boolean = False
            Dim sqlMeal As String
            Dim sqlSplice As String
            Dim sqlStartEnd As String
            Dim StrMeal As String
            Dim StrSplice As String
            Dim StrStartEnd As String
            Dim myColumnClave As Integer = myCommon.fnGetColumnByName(gvTransactions, "ID")
            Dim myColumnTiempoPz As Integer = myCommon.fnGetColumnByName(gvTransactions, "Tiempo Pz")
            Dim myColumnSoldador As Integer = myCommon.fnGetColumnByName(gvTransactions, "Soldador")
            Dim myColumnProducto As Integer = myCommon.fnGetColumnByName(gvTransactions, "Producto")
            Dim myColumnReferencia As Integer = myCommon.fnGetColumnByName(gvTransactions, "Referencia")
            Dim myColumnPieza As Integer = myCommon.fnGetColumnByName(gvTransactions, "PZ")
            Dim myColumnBeginHour As String = myCommon.fnGetColumnByName(gvTransactions, "Hora Inicio")
            Dim myColumnEndHour As String = myCommon.fnGetColumnByName(gvTransactions, "Hora Fin")
            Dim queryInsert As String
            Dim validWelder As Boolean
            Dim activity As String = "AC0"


            DateEfective = tbFromDate.Text
            Machine = cbMachine.SelectedValue
            UserCreate = Session("usr_shortalias")
            Domain = Session("ent_domain")

            If gvTransactions.Rows.Count > 0 Then
                For i = 0 To gvTransactions.Rows.Count - 1

                    clave = ""
                    TiempoPz = ""
                    soldador = ""
                    producto = ""
                    referencia = ""
                    pieza = ""

                    If gvTransactions.Rows(i).Cells(myColumnClave).Text <> "&nbsp;" Then
                        clave = gvTransactions.Rows(i).Cells(myColumnClave).Text
                    End If
                    If gvTransactions.Rows(i).Cells(myColumnSoldador).Text <> "&nbsp;" Then
                        soldador = gvTransactions.Rows(i).Cells(myColumnSoldador).Text
                    End If
                    If gvTransactions.Rows(i).Cells(myColumnProducto).Text <> "&nbsp;" Then
                        producto = gvTransactions.Rows(i).Cells(myColumnProducto).Text
                    End If
                    If gvTransactions.Rows(i).Cells(myColumnReferencia).Text <> "&nbsp;" Then
                        referencia = gvTransactions.Rows(i).Cells(myColumnReferencia).Text
                    End If
                    If gvTransactions.Rows(i).Cells(myColumnPieza).Text <> "&nbsp;" Then
                        pieza = gvTransactions.Rows(i).Cells(myColumnPieza).Text
                    End If
                    If gvTransactions.Rows(i).Cells(myColumnTiempoPz).Text <> "&nbsp;" Then
                        TiempoPz = gvTransactions.Rows(i).Cells(myColumnTiempoPz).Text
                    End If

                    HourBeggin = (TryCast(gvTransactions.Rows(i).FindControl("tbHourBegin"), TextBox)).Text
                    HourEnd = (TryCast(gvTransactions.Rows(i).FindControl("tbHourEnd"), TextBox)).Text
                    Meal = (TryCast(gvTransactions.Rows(i).FindControl("CkMeal"), CheckBox)).Checked
                    splice = (TryCast(gvTransactions.Rows(i).FindControl("CkSplice"), CheckBox)).Checked
                    StartEnd = (TryCast(gvTransactions.Rows(i).FindControl("CkStartEnd"), CheckBox)).Checked

                    If Meal = True Then
                        sqlMeal = "select CODE_VALUE, CODE_CMMT from QAD.CODE_MSTR where UPPER(CODE_FLDNAME) = 'XXPRTM_MEAL' AND UPPER(CODE_DOMAIN) =  '" & Session("ent_domain") & "' and CODE_VALUE  = 'HR1'"
                        myConnection.prOpenOracleDB(Session("ent_index"))
                        myConnection.cmOracle = New OdbcCommand(sqlMeal, myConnection.dbConOracle)
                        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
                        If myConnection.drOracle.HasRows Then
                            StrMeal = myConnection.drOracle("CODE_CMMT")
                        End If
                        myConnection.prCloseOracleDB()
                    Else
                        StrMeal = "0"
                    End If

                    If splice = True Then
                        sqlSplice = "select CODE_VALUE, CODE_CMMT from QAD.CODE_MSTR where UPPER(CODE_FLDNAME) = 'XXPRTM_SPLICE' AND UPPER(CODE_DOMAIN) =  '" & Session("ent_domain") & "' and CODE_VALUE  = 'HR1'"
                        myConnection.prOpenOracleDB(Session("ent_index"))
                        myConnection.cmOracle = New OdbcCommand(sqlSplice, myConnection.dbConOracle)
                        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
                        If myConnection.drOracle.HasRows Then
                            StrSplice = myConnection.drOracle("CODE_CMMT")
                        End If
                        myConnection.prCloseOracleDB()
                    Else
                        StrSplice = "0"
                    End If

                    If StartEnd = True Then
                        sqlStartEnd = "select CODE_VALUE, CODE_CMMT from QAD.CODE_MSTR where UPPER(CODE_FLDNAME) = 'XXPRTM_START_END' AND UPPER(CODE_DOMAIN) =  '" & Session("ent_domain") & "' and CODE_VALUE  = 'HR1'"
                        myConnection.prOpenOracleDB(Session("ent_index"))
                        myConnection.cmOracle = New OdbcCommand(sqlStartEnd, myConnection.dbConOracle)
                        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
                        If myConnection.drOracle.HasRows Then
                            StrStartEnd = myConnection.drOracle("CODE_CMMT")
                        End If
                        myConnection.prCloseOracleDB()
                    Else
                        StrStartEnd = "0"
                    End If

                    ' Validar si ya existe el registro en la tabla xxprtm_hist
                    If HourBeggin <> Nothing And HourEnd <> Nothing Then
                        validWelder = fnValidateWelderOTs(DateEfective, Machine, clave, referencia)
                        If validWelder = False Then
                            queryInsert = <sql>
                                  INSERT INTO QAD.XXPRTM_HIST (
                                    XXPRTM_EFF_DATE, 
                                    XXPRTM_MACHINE,
                                    XXPRTM_ACTIVITY, 
                                    XXPRTM_EMP_ADDR,
                                    XXPRTM__CHR01,
                                    XXPRTM_REFERENCE, 
                                    XXPRTM_START_TIME, 
                                    XXPRTM_END_TIME,  
                                    XXPRTM_MEAL,
                                    XXPRTM_SPLICE,
                                    XXPRTM_START_END,
                                    XXPRTM_CREATE_USR,
                                    XXPRTM_CREATE_DATE,
                                    XXPRTM_DOMAIN, 
                                    PROGRESS_RECID) 
                                VALUES (
                                    TO_DATE('<%= DateEfective %>','DD/MM/YYYY'),
                                    '<%= Machine %>',
                                    '<%= activity %>',
                                    '<%= clave %>',
                                    '<%= TiempoPz %>',
                                    '<%= referencia %>',
                                    '<%= HourBeggin %>',
                                    '<%= HourEnd %>',
                                    <%= StrMeal %>,
                                    <%= StrSplice %>,
                                    <%= StrStartEnd %>,
                                    '<%= UserCreate %>',
                                    TO_DATE(SYSDATE, 'DD/MM/YYYY'),
                                    '<%= Domain %>',
                                    TO_NUMBER(QAD.XXPRTM_HIST_SEQ.NEXTVAL))
                              </sql>
                            Swal = "Se ha registrado con exito"

                        Else
                            queryInsert = <sql>
                                  UPDATE QAD.XXPRTM_HIST SET
                                        XXPRTM_START_TIME = '<%= HourBeggin %>',
                                        XXPRTM_END_TIME = '<%= HourEnd %>',
                                        XXPRTM_MODIF_USR = '<%= UserCreate %>',
                                        XXPRTM_MODIF_DATE = TO_DATE(SYSDATE, 'DD/MM/YYYY'),
                                        XXPRTM_MEAL = <%= StrMeal %>,
                                        XXPRTM_SPLICE = <%= StrSplice %>,
                                        XXPRTM_START_END = <%= StrStartEnd %>,
                                        XXPRTM_ACTIVITY = '<%= activity %>'
                                  WHERE XXPRTM_EFF_DATE = TO_DATE('<%= DateEfective %>','DD/MM/YYYY') AND 
                                        XXPRTM_MACHINE = '<%= Machine %>' AND
                                        XXPRTM_EMP_ADDR = '<%= clave %>' AND
                                        XXPRTM_REFERENCE = '<%= referencia %>'
                              </sql>
                            Swal = "Su registro se ha actualizado"

                        End If

                        prInsertNewDatas(queryInsert)
                        ' Response.Write(queryInsert & "<br>")

                        ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire( 'Correcto!', " & Swal & ", 'success' );</script>")
                    End If
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Protected Sub prInsertNewDatas(query As String)
        Try
            myConnection.prExecuteSQL(query, 2)
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub


    Function fnValidateWelderOTs(DateEfective As String, Machine As String, clave As String, referencia As String)
        Dim validWelder As Boolean
        Dim myStr As String
        myStr = <sql>
                    SELECT * 
                    FROM QAD.XXPRTM_HIST
                    WHERE XXPRTM_EFF_DATE = TO_DATE('<%= DateEfective %>','DD/MM/YYYY') AND 
                          XXPRTM_MACHINE = '<%= Machine %>' AND
                          XXPRTM_EMP_ADDR = '<%= clave %>' AND
                          XXPRTM_REFERENCE = '<%= referencia %>'
                </sql>
        myConnection.prOpenOracleDB(Session("ent_index"))
        myConnection.cmOracle = New OdbcCommand(myStr, myConnection.dbConOracle)
        myConnection.drOracle = myConnection.cmOracle.ExecuteReader
        validWelder = False
        ' validar que exista el registro
        If myConnection.drOracle.HasRows Then
            validWelder = True
        End If

        myConnection.prCloseOracleDB()
        Return validWelder
    End Function

End Class
