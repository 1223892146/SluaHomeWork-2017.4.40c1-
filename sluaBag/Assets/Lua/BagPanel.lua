
--一个面板对应一个表
BasePanel:subClass("BagPanel")

BagPanel.Content = nil
BagPanel.ItemName = nil
BagPanel.ItemDes = nil

--用来存储当前显示的格子
BagPanel.items = {}

BagPanel.nowType = -1
BagPanel.nowId = nil
local nowItems = nil


--初始化方法
function BagPanel:Init(name)
    self.base.Init(self,name)
    if self.isInitEvent == false then
        --没有挂载UI控件的对象需要通过手动去找
        self.Content = self:GetControl("svBag","ScrollRect").transform:Find("Viewport"):Find("Content")
        --加事件
        --关闭按钮
        --self:GetControl("togGem","Toggle")
        self:GetControl("btnClose","Button").onClick:AddListener(
            function()
                self:HideMe()
            end
        )
        self:GetControl("btnUse","Button").onClick:AddListener(
            function()                      --根据不同的道具类型进行不同的操作
                print("type" .. self.nowType);
                if self.nowType == 1 then
                    for i=1, #PlayerData.equips do
                        if self.nowId == PlayerData.equips[i].id and PlayerData.equips[i].num > 0 then
                            PlayerData.equips[i].num = PlayerData.equips[i].num - 1
                        end
                    end
                end

                if self.nowType == 2 then
                    for i=1, #PlayerData.items do
                        if self.nowId == PlayerData.items[i].id and PlayerData.items[i].num > 0 then
                            PlayerData.items[i].num = PlayerData.items[i].num - 1
                        end
                    end
                end

                if self.nowType == 3 then
                    for i=1, #PlayerData.Piece do
                        if self.nowId == PlayerData.Piece[i].id and PlayerData.Piece[i].num > 0 then
                            if PlayerData.Piece[i].num >= 30 then
                                PlayerData.Piece[i].num = PlayerData.Piece[i].num - 30
                                for i=1, #PlayerData.items do
                                    if PlayerData.items[i].id == 4 then
                                        PlayerData.items[i].num = PlayerData.items[i].num + 1   
                                    end
                                end
                            end
                            --PlayerData.Piece[i].num = PlayerData.Piece[i].num - 1
                        end
                    end
                end
                self:ChangeType(self.nowType)
            end
        )
        self:GetControl("btnDiscard","Button").onClick:AddListener(
            function()
                print("type" .. self.nowType);
                if self.nowType == 1 then
                    for i=1, #PlayerData.equips do
                        if self.nowId == PlayerData.equips[i].id and PlayerData.equips[i].num > 0 then
                            PlayerData.equips[i].num = PlayerData.equips[i].num - 1
                        end
                    end
                end

                if self.nowType == 2 then
                    for i=1, #PlayerData.items do
                        if self.nowId == PlayerData.items[i].id and PlayerData.items[i].num > 0 then
                            PlayerData.items[i].num = PlayerData.items[i].num - 1
                        end
                    end
                end

                if self.nowType == 3 then
                    for i=1, #PlayerData.Piece do
                        if self.nowId == PlayerData.Piece[i].id and PlayerData.Piece[i].num > 0 then 
                            PlayerData.Piece[i].num = PlayerData.Piece[i].num - 1
                        end
                    end
                end
                self:ChangeType(self.nowType)
            end
        )

        --单选框事件
        --切页签
        self:GetControl("togEquip","Toggle").onValueChanged:AddListener(
            function(value)
                if value == true    then
                    self:ChangeType(1)
                end
            end
        )    
        self:GetControl("togItem","Toggle").onValueChanged:AddListener(
            function(value)
                if value == true    then
                    self:ChangeType(2)
                end
            end
        )    
        self:GetControl("togPiece","Toggle").onValueChanged:AddListener(
            function(value)
                if value == true    then
                    self:ChangeType(3)
                end
            end
        )   
        self.ItemName = self:GetControl("txtItemName","Text")  
        self.ItemDes = self:GetControl("txtItemDescribe","Text")  
        self.ItemName.text = nil
        self.ItemDes.text = nil

        self.isInitEvent = true     
    end

end

--显示隐藏
function BagPanel:ShowMe(name)
    self.base.ShowMe(self,name)
    if self.nowType == -1  then
        self:ChangeType(2)
    end
end



--逻辑处理函数
--1装备 2道具 3宝石
function BagPanel:ChangeType(type)
    --print("当前类型为"..type)
    --if self.nowType == type then
      --  return
    --end
    --更新之前把老的格子删掉
    for i = 1,#self.items do
        --print("LL"..self.items[i].obj)
        self.items[i]:Destroy()
    end
    self.items = {}

    --再根据当前选择的类型来创建新的格子BagPanel.items
    --根据传入的的type来选择显示数据
    self.nowType = type
    if type == 1 then
        nowItems = PlayerData.equips
    elseif type == 2 then
        nowItems = PlayerData.items
    else
        nowItems = PlayerData.Piece
    end

    
    --创建格子
    for i=1, #nowItems do
        --根据数据创建格子对象
        local grid = ItemGrid:new()

        --实例化格子对象 设置位置
        grid:Init(self.Content,(i-1)%4*150, math.floor((i-1)/4)*150)
        --初始化它的信息数量和图标
        grid:InitData(nowItems[i])

        --把它存起来
        table.insert(self.items,grid)
    end
end

