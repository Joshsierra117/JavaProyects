Imports System.Data.Odbc
Imports System.Data
Imports System.IO
Imports System.Globalization

Partial Class MT02_16_14
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        myGlobal.prGetUserInfo()
        myGlobal.prAllowView(1, body, , form1)
        If Not Page.IsPostBack Then
            'prConfPro()
            fillSoldador()
            fillMch()
        End If
        dsDataSource.ConnectionString = myGlobal.fnConStringOracle(Session("ent_index"))
        dsDataSource.ProviderName = "System.Data.Odbc"
        'Me.DataBind()
        imgExcel.Attributes.Add("OnMouseOver", "this.style.cursor='hand';")
        imgExcel.Attributes.Add("onmouseover", "javascript:return overlib('  Exportar Reporte a Excel.  ')")
        imgExcel.Attributes.Add("onmouseout", "javascript:return nd();")
        imgExcel.Attributes.Add("onfocus", "javascript:this.blur();")
        imgExcel.ImageUrl = myGlobal.fnGetServerName() & "/images/excel07.jpg"
    End Sub


    ''' <summary>
    ''' método para llenar el filtro del soldador 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub fillSoldador()
        Dim myStrSQL As String
        Dim domain As String
        domain = Session("ent_domain")
        myStrSQL = <sql>
                       select UPPER (CL_EMP) as CL_SOLDADOR , DE_NOMBRE || ' ' || DE_APELLIDO_PATERNO || ' ' || DE_APELLIDO_MATERNO AS DE_SOLDADOR 
                       from qad.ad_m_em_##_QAD where upper(cl_puesto) IN ('SOR','SOC','FSOL') and upper(cl_status) = 'AC' and upper(CL_ENTIDAD) = 'SCM' and CL_DEPTO = 'CSOL' ORDER BY DE_APELLIDO_PATERNO ASC
                   </sql>
        myGlobal.prOpenOracleDB(Session("ent_index"))
        myGlobal.cmOracle = New OdbcCommand(myStrSQL, myGlobal.dbConOracle)
        myGlobal.drOracle = myGlobal.cmOracle.ExecuteReader
        cbColab.Items.Add(New ListItem("Selecciona un soldador", ""))
        While myGlobal.drOracle.Read()
            cbColab.Items.Add(New ListItem(myGlobal.drOracle("CL_SOLDADOR") + "-" + myGlobal.drOracle("DE_SOLDADOR"), myGlobal.drOracle("CL_SOLDADOR")))
        End While
        myGlobal.dbConOracle.Close()
    End Sub

    ''' <summary>
    ''' método para llenar el filtro de maquina, usa code mstr para agregar mas maquina de forma dinamica por meido del campo wc_wkctr
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub fillMch()
        Dim myStr As String
        Dim dynamicMch As String
        myStr = "SELECT CODE_CMMT FROM QAD.CODE_MSTR " & _
                        "WHERE UPPER(CODE_FLDNAME) = 'XXWC_WKCTR' AND " & _
                            "UPPER(CODE_VALUE) = 'MCH' AND " & _
                            "UPPER(CODE_DOMAIN) = '" & Session("ent_domain") & "' "

        myGlobal.prOpenOracleDB(Session("ent_index"))
        myGlobal.cmOracle = New OdbcCommand(myStr, myGlobal.dbConOracle)
        myGlobal.drOracle = myGlobal.cmOracle.ExecuteReader
        While myGlobal.drOracle.Read()
            dynamicMch = myGlobal.drOracle("CODE_CMMT")
        End While
        myGlobal.prCloseOracleDB()

        myStr = "select wc_mch, wc_desc from qad.wc_mstr " & _
                    "where upper(wc_domain) = '" & Session("ent_domain") & "' AND UPPER(WC__CHR03) = 'SCM' AND UPPER(wc_wkctr) IN (" & dynamicMch & ") " & _
                    "order by wc_desc desc"

        myGlobal.prOpenOracleDB(Session("ent_index"))
        myGlobal.cmOracle = New OdbcCommand(myStr, myGlobal.dbConOracle)
        myGlobal.drOracle = myGlobal.cmOracle.ExecuteReader
        cbMachine.Items.Add(New ListItem("Seleccionar una opción", " "))
        While myGlobal.drOracle.Read()
            cbMachine.Items.Add(New ListItem(myGlobal.drOracle("wc_mch"), myGlobal.drOracle("wc_mch")))
        End While
        myGlobal.prCloseOracleDB()
    End Sub

    ''' <summary>
    ''' Funcionq que contiene los filtros y la consulta para llenar la tabla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function fillGridOne()
        Dim DateEffecFrom As String = tbFromDate.Text
        Dim DateEffecTo As String = tbToDate.Text
        Dim Machine As String = cbMachine.SelectedValue
        Dim Colab As String = cbColab.SelectedValue
        Dim filter As String = ""
        Dim myStr As String

        If DateEffecFrom <> "" And DateEffecTo = "" Then
            filter += " and XXPRTM_EFF_DATE = to_date('" & DateEffecFrom & "','DD/MM/YYYY') "
        ElseIf DateEffecFrom = "" And DateEffecTo <> "" Then
            filter += " and XXPRTM_EFF_DATE = to_date('" & DateEffecTo & "','DD/MM/YYYY') "
        ElseIf DateEffecFrom <> "" And DateEffecTo <> "" Then
            filter += " and XXPRTM_EFF_DATE >= to_date('" & DateEffecFrom & "','DD/MM/YYYY') and XXPRTM_EFF_DATE <= to_date('" & DateEffecTo & "','DD/MM/YYYY') "
        Else
            filter = ""
        End If
        If Machine <> " " And Machine <> "" Then
            filter += " and xxprtm_machine = '" & Machine & "'"
        End If
        If Colab <> "" Then
            filter += " and XXPRTM_EMP_ADDR = '" & Colab & "'"
        End If

        myStr = "SELECT XXPRTM_EFF_DATE, XXPRTM_MACHINE, XXPRTM_EMP_ADDR, DE_SOLDADOR, XXPRTM_ACTIVITY, CODE_CMMT, XXPRTM_REFERENCE, XXPRTM_START_TIME," & _
                    " XXPRTM_END_TIME, CASE WHEN SUBSTR(DIF_DATE,0,1) = '-' THEN  DIF_DATE * (-1) ELSE DIF_DATE END DIF_DATE, CL_PRODUCTO, CN_TRANSACCION, XXPRTM__CHR01, " & _
                    " ROUND(((CASE WHEN SUBSTR(DIF_DATE,0,1) = '-' THEN  DIF_DATE * (-1) ELSE DIF_DATE END)/XXPRTM__CHR01 ),1) AS PZ_META,ROUND((CN_TRANSACCION*XXPRTM__CHR01 ),1) AS MIN_TEORICA " & _
                "FROM ( " & _
                    " SELECT TO_CHAR (XXPRTM_EFF_DATE, 'DD/MM/YYYY') AS XXPRTM_EFF_DATE, XXPRTM_MACHINE, XXPRTM_EMP_ADDR, DE_NOMBRE || ' ' || DE_APELLIDO_PATERNO || ' ' || DE_APELLIDO_MATERNO AS DE_SOLDADOR, " & _
                        " XXPRTM_ACTIVITY, CODE_CMMT, XXPRTM_REFERENCE, XXPRTM_START_TIME, XXPRTM_END_TIME, " & _
                        "(((substr(XXPRTM_START_TIME, 0, 2)) - (CASE WHEN substr(XXPRTM_END_TIME, 0, 2) = 00 THEN 24 ELSE TO_NUMBER(substr(XXPRTM_END_TIME, 0, 2)) END)) * (-60)) - ((TO_NUMBER(substr(XXPRTM_START_TIME, 4))) - (TO_NUMBER(substr(XXPRTM_END_TIME, 4)))) - ((XXPRTM_MEAL + XXPRTM_SPLICE) + XXPRTM_START_END) AS DIF_DATE, " & _
                        " CL_PRODUCTO, SUM (CN_TRANSACCION) AS CN_TRANSACCION, CASE WHEN XXPRTM__CHR01 = ' ' THEN '1' ELSE XXPRTM__CHR01 END XXPRTM__CHR01 " & _
                    " FROM QAD.XXPRTM_HIST " & _
                    " LEFT JOIN qad.AD_M_EM_##_QAD ON UPPER (XXPRTM_EMP_ADDR) = CL_EMP " & _
                    " LEFT JOIN QAD.IC_D_WO_##_A_QAD ON UPPER (NU_FACTURA) = XXPRTM_EMP_ADDR AND NU_OV_OT = XXPRTM_REFERENCE AND FE_TRANSACCION = XXPRTM_EFF_DATE and CL_TIPO_TRANSACC = 'RCT-WO' " & _
                    " LEFT JOIN QAD.CODE_MSTR ON UPPER (XXPRTM_ACTIVITY) = CODE_VALUE AND UPPER (CODE_FLDNAME) = 'XXPRTM_ACTIVITY' AND CODE_DOMAIN = '" & Session("ent_domain") & "' " & _
                    " WHERE XXPRTM_DOMAIN = '" & Session("ent_domain") & "' AND XXPRTM_START_TIME <> ' ' AND XXPRTM_END_TIME <> ' ' AND UPPER (XXPRTM_ACTIVITY) = 'AC0' AND (XXPRTM__CHR01 <> ' ' OR XXPRTM__CHR01 <> '') " & filter & _
                    " GROUP BY UPPER (NU_FACTURA), XXPRTM_EFF_DATE, XXPRTM_MACHINE, XXPRTM_EMP_ADDR, DE_APELLIDO_PATERNO, DE_APELLIDO_MATERNO, DE_NOMBRE, XXPRTM_ACTIVITY, CODE_CMMT, XXPRTM_REFERENCE, " & _
                        " CL_PRODUCTO, XXPRTM_MEAL, XXPRTM_SPLICE, XXPRTM_START_END, XXPRTM_START_TIME, XXPRTM_END_TIME, XXPRTM__CHR01 ORDER BY XXPRTM_EFF_DATE DESC) "
        'Response.Write(myStr)
        Return myStr
    End Function

    ''' <summary>
    ''' método para llenar la tabla cuando detecta el click del boton btSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        If tbFromDate.Text <> Nothing Or tbToDate.Text <> Nothing Then
            Cache.Remove(dsDataSource.CacheKeyDependency.ToString)
            gvGridView.DataSourceID = "dsDataSource"
            Cache.Insert(dsDataSource.CacheKeyDependency, dsDataSource)
            dsDataSource.SelectCommand = fillGridOne()
            'Response.Write(fillGridOne())
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "script123", "<script language='JavaScript'>Swal.fire({ icon:  'warning', title: 'Aviso...', text:     'El campo Fecha es obligatorio',})</script>")
        End If
    End Sub

    ''' <summary>
    ''' método para agrupar las columnas por el rfc o id del colaborador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvGridView_PreRender(sender As Object, e As EventArgs) Handles gvGridView.PreRender
        Me.gvGridView.GroupedColumns.Add(Me.gvGridView.Columns.ColumnByName("Soldador"))
        ''' vamos a calcular el cumplimiento por ot
        Dim myColumnDiffMin As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Tpo Total"))
        Dim myColumnMinTeor As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Min Prog"))
        Dim myColumnCumpli As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Cumplimiento"))
        Dim myColumnCode As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Soldador"))
        Dim myColumnJornada As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("% Jornada"))
        Dim myColumnPonderado As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Ponderado"))
        Dim diffMin As Decimal = 0
        Dim minTeor As Double = 0.0
        Dim cumplimiento As Double
        Dim soldador As String = ""
        Dim ponderado As Double = 0
        ''' mostrar radio button list
        If gvGridView.GroupedColumns.Count > 0 Then
            RadioButtonList1.Visible = True
        Else
            RadioButtonList1.Visible = True
        End If

        Try
            If gvGridView.Items.Count > 0 Then
                For i = 0 To gvGridView.Items.Count - 1
                    diffMin = CDec(gvGridView.Items(i).Cells(myColumnDiffMin).Text)
                    minTeor = CDbl(gvGridView.Items(i).Cells(myColumnMinTeor).Text)
                    cumplimiento = minTeor / diffMin
                    gvGridView.Items(i).Cells(myColumnCumpli).Text = (Math.Round(cumplimiento, 2)) * 100 & " %"
                    soldador = gvGridView.Items(i).Cells(myColumnCode).Text
                    ponderado = fnTotalTimeDiff(soldador, diffMin)
                    gvGridView.Items(i).Cells(myColumnJornada).Text = (Math.Round(ponderado, 2)) * 100 & " %"
                    gvGridView.Items(i).Cells(myColumnPonderado).Text = (Math.Round((cumplimiento * ponderado), 2)) * 100 & "%"
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.toString)
        End Try
    End Sub

    ''' <summary>
    ''' Agrupar dependiendo el valor del radio button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        Dim i As Short
        Dim myGroupedCol(gvGridView.GroupedColumns.Count - 1) As C1.Web.C1WebGrid.C1BoundColumn
        Select Case RadioButtonList1.SelectedIndex
            Case 0
                For i = 0 To myGroupedCol.Length - 1
                    myGroupedCol(i) = gvGridView.GroupedColumns(i)
                    myGroupedCol(i).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed
                Next
            Case 1
                For i = 0 To myGroupedCol.Length - 1
                    myGroupedCol(i) = gvGridView.GroupedColumns(i)
                    myGroupedCol(i).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
                Next
        End Select
        For i = 0 To myGroupedCol.Length - 1
            gvGridView.GroupedColumns.Remove(myGroupedCol(i))
            gvGridView.GroupedColumns.Add(myGroupedCol(i))
        Next
    End Sub

    ''' <summary>
    ''' método que va a servir para llenar el cumplimiento del soldador y algo mas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvGridView_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvGridView.DataBound
        Dim mySumTotalKG As Decimal
        Dim mySumTotalPZ As Decimal
        Dim myDifMin As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Tpo Total"))
        Dim myCol As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Soldador"))
        Dim myColaborate As String = ""
        Dim myColValidate As String = ""
        If gvGridView.Items.Count > 0 Then
            For i = 0 To gvGridView.Items.Count - 1
                ''' llenamos las variable por primera vez
                myColaborate = gvGridView.Items(i).Cells(myCol).Text
                ''' en caso de encontrar alguna coincidencia sumamos Tpo Total
                If myColValidate = myColaborate Then
                    mySumTotalKG += CDec(gvGridView.Items(i).Cells(myDifMin).Text)
                Else
                    mySumTotalKG = CDec(gvGridView.Items(i).Cells(myDifMin).Text)
                End If
                myColValidate = myColaborate
            Next
            'gvGridView.Columns(myTotalKG).FooterText = mySumTotalKG.ToString("N2", CultureInfo.InvariantCulture)
            'gvGridView.Columns(myTotalKG).FooterStyle.HorizontalAlign = HorizontalAlign.Center
            'gvGridView.Columns(myTotalPZ).FooterText = mySumTotalPZ.ToString("N2", CultureInfo.InvariantCulture)
            'gvGridView.Columns(myTotalPZ).FooterStyle.HorizontalAlign = HorizontalAlign.Center
        End If
    End Sub

    ''' <summary>
    ''' Funcion que funciona para obtener el total de tiempo por soldador
    ''' </summary>
    ''' <param name="myCodeCol">es el rfc o id del colaborado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function fnTotalTimeDiff(ByVal myCodeCol As String, ByVal diffMin As Double)
        Dim myStr As String
        Dim DateEffecFrom As String = tbFromDate.Text
        Dim DateEffecTo As String = tbToDate.Text
        Dim Machine As String = cbMachine.SelectedValue
        Dim Colab As String = cbColab.SelectedValue
        Dim filter As String = ""
        Dim totalMin As Decimal = 0

        If DateEffecFrom <> "" And DateEffecTo = "" Then
            filter += " and XXPRTM_EFF_DATE = to_date('" & DateEffecFrom & "','DD/MM/YYYY') "
        ElseIf DateEffecFrom = "" And DateEffecTo <> "" Then
            filter += " and XXPRTM_EFF_DATE = to_date('" & DateEffecTo & "','DD/MM/YYYY') "
        ElseIf DateEffecFrom <> "" And DateEffecTo <> "" Then
            filter += " and XXPRTM_EFF_DATE >= to_date('" & DateEffecFrom & "','DD/MM/YYYY') and XXPRTM_EFF_DATE <= to_date('" & DateEffecTo & "','DD/MM/YYYY') "
        Else
            filter = ""
        End If
        If Machine <> " " And Machine <> "" Then
            filter += " and xxprtm_machine = '" & Machine & "'"
        End If
        If Colab <> "" Then
            filter += " and XXPRTM_EMP_ADDR = '" & Colab & "'"
        End If

        'GMU 20230204 ( - AND (XXPRTM__CHR01 <> ' ' OR XXPRTM__CHR01 <> '') - )

        myStr = "SELECT SUM(DIF_DATE) AS TOTAL FROM (SELECT CASE WHEN SUBSTR(DIF_DATE,0,1) = '-' THEN  DIF_DATE * (-1) ELSE DIF_DATE END DIF_DATE  FROM ( " & _
                    " SELECT A.XXPRTM_EMP_ADDR, " & _
                    " (((substr(XXPRTM_START_TIME, 0, 2)) - (CASE WHEN substr(XXPRTM_END_TIME, 0, 2) = 00 THEN 24 ELSE TO_NUMBER(substr(XXPRTM_END_TIME, 0, 2)) END)) * (-60)) - ((TO_NUMBER(substr(XXPRTM_START_TIME, 4))) - (TO_NUMBER(substr(XXPRTM_END_TIME, 4)))) - ((XXPRTM_MEAL + XXPRTM_SPLICE) + XXPRTM_START_END) AS DIF_DATE " & _
                " FROM QAD.XXPRTM_HIST A " & _
                " WHERE A.XXPRTM_DOMAIN = '" & Session("ent_domain") & "' AND A.XXPRTM_START_TIME <> ' ' AND A.XXPRTM_END_TIME <> ' ' AND UPPER (A.XXPRTM_ACTIVITY) = 'AC0' " & _
                " AND (XXPRTM__CHR01 <> ' ' OR XXPRTM__CHR01 <> '')" & filter & _
                " /* GROUP BY A.XXPRTM_MEAL, A.XXPRTM_SPLICE, A.XXPRTM_START_END, A.XXPRTM_START_TIME, A.XXPRTM_END_TIME */)  B " & _
                " WHERE B.XXPRTM_EMP_ADDR = '" & myCodeCol & "' )"
        myGlobal.prOpenOracleDB(Session("ent_index"))
        myGlobal.cmOracle = New OdbcCommand(myStr, myGlobal.dbConOracle)
        myGlobal.drOracle = myGlobal.cmOracle.ExecuteReader
        While myGlobal.drOracle.Read()
            If myCodeCol = "SEEF9606" Or myCodeCol = "BAGL8511" Then
                'Response.Write(myStr)
                'Response.Write(diffMin & "/" & CDec(myGlobal.drOracle("TOTAL")) & "<br>")
            End If
            totalMin = diffMin / CDec(myGlobal.drOracle("TOTAL"))
        End While
        myGlobal.prCloseOracleDB()
        Return totalMin
    End Function

    Protected Sub imgExcel_Click(sender As Object, e As ImageClickEventArgs) Handles imgExcel.Click
        Dim myTitle As String = myHeaderSm.Title
        prExportExcel(gvGridView, myTitle)
    End Sub

    ''' <summary>
    ''' metodo para generar el excel de la tabla
    ''' </summary>
    ''' <param name="gvGridView"></param>
    ''' <param name="myNameRep"></param>
    ''' <remarks></remarks>
    Sub prExportExcel(ByVal gvGridView As C1.Web.C1WebGrid.C1WebGrid, ByVal myNameRep As String)

        gvGridView.GroupingStyle.BackColor = Drawing.Color.White
        gvGridView.HeaderStyle.BackColor = Drawing.Color.White
        gvGridView.AlternatingItemStyle.BackColor = Drawing.Color.GhostWhite
        gvGridView.ItemStyle.BorderStyle = BorderStyle.Groove
        gvGridView.BorderColor = Drawing.Color.Black
        gvGridView.BorderStyle = BorderStyle.Groove

        'comenzar con la descarga del grid a excel
        Dim attachment As String = "attachment; filename=" & Mid(myNameRep, 5, Len(myNameRep)) & "_" & Date.Today.ToString("dd/MM/yyyy") & ".xls"
        Response.Clear()
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = "UTF-8" 'para que permita acentos y ñ's
        Response.ContentEncoding = System.Text.Encoding.Default

        Dim sw As StringWriter = New StringWriter
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm


        gvGridView.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(gvGridView)
        frm.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()

    End Sub

    ''' <summary>
    ''' método para hacer formula custom en el ponderado 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvGridView_GroupAggregate(sender As Object, e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles gvGridView.GroupAggregate
        Dim myColPonIndex As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Ponderado"))
        Dim myColDiffMinIndex As Integer = gvGridView.Columns.IndexOf(gvGridView.Columns.ColumnByName("Tpo Total"))
        Dim j As Integer
        Dim myPonText As String
        Dim myPonSum As Decimal

        For j = e.StartItemIndex To e.EndItemIndex
            myPonText = gvGridView.Items(j).Cells(myColPonIndex).Text
            myPonSum += myPonText.Remove(myPonText.Length - 1, 1)
        Next

        Select Case e.Col.HeaderText
            Case "Ponderado"
                If myPonSum >= 100 Then
                    e.Text = "100%"
                Else
                    e.Text = myPonSum & "%"
                End If
        End Select
    End Sub

End Class
