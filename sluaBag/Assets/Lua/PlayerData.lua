PlayerData = {}
--我们目前只做背包功能 所以只需要它的道具信息即可

PlayerData.equips = {}
PlayerData.items = {}
PlayerData.Piece = {}

--为玩家数据写了一个初始化方法 以后直接该这里的数据来源即可
function PlayerData:Init()
    --道具信息 不管存本地还是存服务器都不会把道具所有的信息都存进去
    --道具ID和道具数量  

    --目前因为没有服务器 为了测试 我们就写死道具数据作为玩家信息
    table.insert(self.equips,{id = 1, num = 1})
    table.insert(self.equips,{id = 2, num = 2})

    table.insert(self.items,{id = 3, num = 10})
    table.insert(self.items,{id = 4, num = 5})

    table.insert(self.Piece,{id = 5, num = 99})
end

PlayerData:Init()
print("kk"..#PlayerData.equips)
print(PlayerData.equips[1].id)