import re, requests, bs4, webbrowser

produto = "ssd-240gb-kingston"
resp = requests.get("https://lista.mercadolivre.com.br/"+produto)

bsBuild = bs4.BeautifulSoup(resp.text, "html.parser")

selectSpan = bsBuild.select(".ui-search-price__second-line > span > .price-tag-amount > .price-tag-fraction")


soma = 0
counte = len(selectSpan) - 1
count = 0
media = 0

for i in range(0, 20, 2):
    if (i > 1):
        percent50 = float(selectSpan[i].text)*0.5
        if (media < percent50):
            continue
    soma += float(selectSpan[i].text)
    print(selectSpan[i].text)
    count += 1
    media = soma / count

min = 1
for i in range(0, 20, 2):
    if i == 0:
        min = float(selectSpan[i].text)
    else:
        if float(selectSpan[i].text) < min:
            min = float(selectSpan[i].text)

max = 1
for i in range(0, 20, 2):
    if i == 0:
        max = float(selectSpan[i].text)
    else:
        if float(selectSpan[i].text) > max:
            max = float(selectSpan[i].text)



print(f"Media de preco: R${soma / 10}")
print(f"Produto com menor preço: R${min}")
print(f"Produto com maior preço: R${max}")
