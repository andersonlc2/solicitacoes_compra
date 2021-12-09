import pyodbc


#data_connection = ("DRIVER={SQL Server};SERVER=DESKTOP-3M626FO\SQLEXPRESS;DATABASE=D:\BACKUP\PROJETOS\C\LIVRO_C#\ADO_NET\ADO_NET\APP.DATA\DATABASE.MDF;")
data_connection = ("DRIVER={SQL Server};SERVER=DESKTOP-3M626FO\SQLEXPRESS;DATABASE=D:\BACKUP\PROJETOS\C\C#\SOLICITACOES_COMPRA\DATA\APP_DATA_HOUSE\DBSIMPLE_HOUSE.MDF;")

connection = pyodbc.connect(data_connection)
print("Connection sussessful")

cursor = connection.cursor()

Cnpj = 12012025000165
Nome = "Python"

#comando = f"INSERT INTO Fornecedor(Cnpj, Nome) Values('{Cnpj}', '{Nome}')"
comando = f"SELECT * FROM Produto"

cursor.execute(comando)

for produto in cursor.fetall():
    print(produto.Descricao)


"""while True:
    row = cursor.fetchone()
    if not row:
        break
    print(row.nome)"""
"""while True:
    row = cursor.fetchone()
    if not row:
        break
        print(row.nome)"""

cursor.commit()
