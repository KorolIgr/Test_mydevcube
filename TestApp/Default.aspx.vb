Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim connetionString As String
        Dim connection As SqlConnection
        Dim command As SqlCommand
        Dim adapter As New SqlDataAdapter
        Dim ds As New DataSet
        Dim i As Integer
        Dim sql As String

        connetionString = "Data Source=IGOR_PC\SQLEXPRESS;Initial Catalog=TT;Integrated Security=True"
        sql = "SELECT 
	                T2.Name, 
	                T2.Address, 
	                T1.Number, 
	                T1.Date, 
	                T1.Description, 
	                T1.Amount 
                FROM 
	                [dbo].[Orders] as T1
                LEFT JOIN 
	                [dbo].[Customers] as T2
                ON 
	                T1.Customer_ID = T2.Id"

        connection = New SqlConnection(connetionString)

        Try
            connection.Open()
            command = New SqlCommand(sql, connection)
            adapter.SelectCommand = command
            adapter.Fill(ds)
            adapter.Dispose()
            command.Dispose()
            connection.Close()


            Dim Table As New StringBuilder
            Table.Append("<table border=1><tr>")
            Table.Append("<th>Customer Name</th><th>Customer Address</th><th>Order number</th><th>Order date</th><th>Order Description</th><th>Order amount</th>")

            For i = 0 To ds.Tables(0).Rows.Count - 1

                Table.Append("<tr>")
                Table.Append("<td>" & ds.Tables(0).Rows(i).Item(0) & "</td>")
                Table.Append("<td>" & ds.Tables(0).Rows(i).Item(1) & "</td>")
                Table.Append("<td>" & ds.Tables(0).Rows(i).Item(2) & "</td>")
                Table.Append("<td>" & ds.Tables(0).Rows(i).Item(3) & "</td>")
                Table.Append("<td>" & ds.Tables(0).Rows(i).Item(4) & "</td>")
                Table.Append("<td>" & ds.Tables(0).Rows(i).Item(5) & "</td>")

            Next

            Table.Append("</table>")


            PlaceHolder1.Controls.Add(New Literal With {.Text = Table.ToString})

        Catch ex As Exception
            MsgBox("Can not open connection ! ")
        End Try



    End Sub
End Class