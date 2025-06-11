@echo off
set cursor="C:\Users\%USERNAME%\AppData\Local\Programs\Cursor\Cursor.exe"
start "" %cursor% "%1"
timeout /t 1 >nul
start "" %cursor% "%2"
