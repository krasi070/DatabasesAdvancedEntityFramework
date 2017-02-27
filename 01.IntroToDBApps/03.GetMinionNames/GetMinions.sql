USE MinionsDB

SELECT m.Name, m.Age
FROM Minions AS m
INNER JOIN VilliansMinions AS vm
ON m.Id = vm.MinionId
INNER JOIN Villians AS v
ON vm.VillianId = v.Id
WHERE v.Id = @VillianId