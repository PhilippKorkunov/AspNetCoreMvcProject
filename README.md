# AspNetCoreMvcProject


Краткое руководство по проекту


Строка подключения к БД указывается в appsettins.json в поле с ключом "ConnectionStrings".
Взаимодействие с БД происходит с помощью Entity Framework

Проект состоит из 3 слоев:
1) Слой даты, на котором формируется контекст
2) Бизнесс слой, на которм создан репозиторий
3) Слой презентации, на котором происзодит фзаимодействие фронта с базой данных через контроллеры:
   1)HomeConttoller содержит методы:
      1)Index - вызывается для начальной стрницы. Передает модели главных депатраментов
      2)Departmnt - вызывается для отображений структуры департамента (информации о нем, информации о дочерних департаметах и информации о сотрудниках этого департамента)
      
   2)CreateOrEditPageController содержит методы CreateOrEditDepartment и CreateOrEditEmployee для взаимодействия с формой о департаменте и сотруднике соответственно.
     В контроллерер определяется, есть ли запись с аналогичным первичным ключем: если есть, то будет вызываться обновление записи, иначе запись будет добавлена