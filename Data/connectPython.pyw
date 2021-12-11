from pricesMain import getPrice
import mysql.connector


data_connection = mysql.connector.connect(
    host="localhost",
    database="dbsolicit",
    user="root",
    password="admin123"
)

if data_connection.is_connected():
    db_info = data_connection.get_server_info()
    #print("Connection Sussessful: MySQL "+db_info+" version")
    cursor = data_connection.cursor()
    cursor.execute("SELECT * FROM produto")
    resultset = cursor.fetchall()
    #print(resultset)
    for produto in resultset:
        PrecoMedio, MaiorPreco, MenorPreco = getPrice(produto[1])
        #print(PrecoMedio, MaiorPreco, MenorPreco)
        cursor.execute(f"UPDATE Produto SET PrecoMedio={PrecoMedio}, MaiorPreco={MaiorPreco}, MenorPreco={MenorPreco} WHERE ID={produto[0]}")
        data_connection.commit()

if data_connection.is_connected():
    cursor.close()
    data_connection.close()
    #print("Connection finish")
