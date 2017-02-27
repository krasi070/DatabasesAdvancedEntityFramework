USE MinionsDB

SELECT v.Name, COUNT(m.Id) AS MinionsCount
FROM Villians AS v
INNER JOIN VilliansMinions AS vm
ON v.Id = vm.VillianId
INNER JOIN Minions AS m
ON vm.MinionId = m.Id
GROUP BY v.Name
HAVING COUNT(m.Id) > 3
ORDER BY COUNT(m.Id) DESC