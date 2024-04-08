import json
import os
import csv




def isfloat(s):  # 浮動小数点数値かどうかを判定する関数
    try:
        float(s)  # 試しにfloat関数で文字列を変換
    except ValueError:
        return False  # 失敗すれば False
    else:
        return True  # 上手くいけば True
    



try:
    csvPath = "." #使用ディレクトリによって書き換える
    jsonPath = "" #使用ディレクトリによって書き換える
    files = os.listdir(csvPath)
    for csvfile in files:
        if csvfile.endswith('.csv'):
            with open(csvPath + '/' + csvfile, encoding="utf-8") as f:
                reader = csv.reader(f)
                csvlist = [row for row in reader]
                columnCount = len(csvlist[0])      #列数
                rowCount = len(csvlist)            #行数
                columnNames = []                    #カラム名配列
                dataList = []                       #データを格納する配列

                #カラム名の抽出
                for columnIndex in range(0, columnCount):
                    columnNames += [csvlist[1][columnIndex]]

                #データをカラム名と紐づけて連想配列に格納
                for rowIndex in range(2, rowCount):
                    valueDict = {}
                    for columnIndex in range(0, columnCount):
                        cellValue = csvlist[rowIndex][columnIndex]
                        # 基本的に全て文字列型として入力される
                        # 数値ならInt型へ変換
                        if cellValue.isdecimal():
                            cellValue = int(cellValue)
                        # 小数ならFloat型へ変換
                        elif isfloat(cellValue):
                            cellValue = float(cellValue)
                        valueDict[columnNames[columnIndex]] = cellValue
                    dataList += [valueDict]

                #JSONファイルに出力
                fileTitle = os.path.splitext(csvfile)[0] + '.json'
                with open(jsonPath + fileTitle, 'w', encoding='utf_8_sig') as f:
                    json.dump(dataList, f, ensure_ascii=False, indent=4)
                print(fileTitle)


except Exception as e:
    print(e)





