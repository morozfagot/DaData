В проекте описан один контроллер с единственным гет экшоном, принимающим один параметр с двумя полями (string адрес и int id) в теле запроса. Он в свою очередь через библиотеку mediatr вызывает хэндлер на уровне application, в обязанности которого входит работа с репозиториями, сервисами и мапперами. Бизнес логику связанную с обращением к стороннему ресурсу DaData вынес в сервис на уровень инфраструктуры, который подключаю через внедрение зависимостей, учитывая инверсию зависимостей.