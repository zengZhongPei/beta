
---------- 需要手动运行创建用户 ---------



-- 创建一个test localhost 用户
CREATE USER 'test'@'localhost' IDENTIFIED BY 'test123';
-- 为创建的test localhost 用户授权
GRANT ALL PRIVILEGES ON *.* TO 'test'@'localhost' WITH GRANT OPTION;

-- 为在docker-compose中创建的test % 用户授权
GRANT ALL PRIVILEGES ON *.* TO 'test'@'%' WITH GRANT OPTION;

-- 针对Navicat修改mysql的加密规则不然Navicat无法连接
ALTER USER 'test'@'localhost' IDENTIFIED BY 'password' PASSWORD EXPIRE NEVER;
ALTER USER 'test'@'%' IDENTIFIED BY 'password' PASSWORD EXPIRE NEVER;
ALTER USER 'test'@'%' IDENTIFIED WITH mysql_native_password BY 'test123';
ALTER USER 'test'@'localhost' IDENTIFIED WITH mysql_native_password BY 'test123';


