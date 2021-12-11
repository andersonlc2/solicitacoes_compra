import requests, bs4


#produto = "ssd-240gb-kingston"

def getPrice(produtoBanco):
    produto = produtoBanco.replace(' ', '-')
    resp = requests.get("https://lista.mercadolivre.com.br/"+produto)

    bsBuild = bs4.BeautifulSoup(resp.text, "html.parser")

    selectSpan = bsBuild.select(".ui-search-price__second-line > span > .price-tag-amount > .price-tag-fraction")

    lisInt = []
    for i in selectSpan:
        temp= ""
        for j in i.text:
            if j != ".":
                temp = temp + j
        lisInt.append(int(temp))

    soma = 0
    count = 0
    media = 0

    for i in range(0, 20, 2):
        if (i > 1):
            percent50 = lisInt[i]*0.5
            if (media < percent50):
                continue
        soma += lisInt[i]
        count += 1
        media = soma / count

    min = 1
    for i in range(0, 20, 2):
        if i == 0:
            min = lisInt[i]
        else:
            if lisInt[i] < min:
                min = lisInt[i]

    max = 1
    for i in range(0, 20, 2):
        if i == 0:
            max = lisInt[i]
        else:
            if lisInt[i] > max:
                max = lisInt[i]

    return soma/10, max, min
