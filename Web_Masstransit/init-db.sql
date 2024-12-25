-- init-db.sql

-- Создание базы данных для менеджера
SELECT 'CREATE DATABASE manager_db'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'manager_db')\gexec

-- Создание базы данных для директора
SELECT 'CREATE DATABASE director_db'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'director_db')\gexec
