--insert into Users (udid, parametersAge, parametersCountry, parametersGender, spentGameMoney, spentMoney, earnedGameMoney)
--select udid, parametersAge, parametersCountry, parametersGender, parametersIncome, parametersIncome,parametersIncome
--from Events 
--where event_id =2


--UPDATE
--    Users
--SET
--	wins = Table_B.SUMM
--FROM
--    Users AS Table_A
--    INNER JOIN (select count(*) AS SUMM, udid as ud  from Events where event_id=4 and parametersWin=true group by udid) AS Table_B
--        ON Table_A.udid = Table_B.ud


--update 
--Users
--set isCheater = 'true'
--from Users where earnedGameMoney<spentGameMoney

select count(*)
from Users where Tier='Tier0'