from pricesMain import getPrice
import pyodbc
import time
import os


#os.system("net start 'SQL SERVER (SQLEXPRESS)'")

#import subprocess
#subprocess.call(['runas', '/user:andersonescae@hotmail.com', "net start 'SQL SERVER (SQLEXPRESS)'"])

data_connection = ("DRIVER={SQL Server};SERVER=DESKTOP-3M626FO\SQLEXPRESS;DATABASE=D:\BACKUP\PROJETOS\C\C#\SOLICITACOES_COMPRA\DATA\APP.DATA\DBSIMPLE.MDF;")

pyodbc.pooling = False
connection = pyodbc.connect(data_connection)
print("Connection sussessful")
cursor = connection.cursor()

comando = "SELECT * FROM Produto"
cursor.execute(comando)

for produto in cursor.fetchall():
    PrecoMedio, MaiorPreco, MenorPreco = getPrice(produto.Descricao)
    cursor.execute(f"UPDATE Produto SET PrecoMedio={PrecoMedio}, MaiorPreco={MaiorPreco}, MenorPreco={MenorPreco} WHERE ID={produto.ID}")
    print(f"{produto.Descricao} = m:{PrecoMedio} >{MaiorPreco} <{MenorPreco}")

print("End...")

cursor.commit()
connection.close()
