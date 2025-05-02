#!/bin/bash

# Скрипт для вывода структуры проекта в файл tree.txt
# Выполнять из корня проекта

# Проверка наличия утилиты tree
if ! command -v tree &> /dev/null
then
    echo "Утилита 'tree' не установлена. Установи её через 'brew install tree' (macOS) или 'sudo apt install tree' (Linux)."
    exit 1
fi

# Сохраняем структуру в файл
tree -a -I 'bin|obj|node_modules|.git|.DS_Store|*.pyc|__pycache__' > project-structure.txt

echo "✅ Структура сохранена в файл project-structure.txt"

