
--用到之前讲过的知识 Object
--生成一个table 继承Object 主要目的是要它里面实现的继承方法subClass和new
Object:subClass("ItemGrid")

--成员变量
ItemGrid.Obj = nil
ItemGrid.imgIcon = nil
ItemGrid.Text = nil

--成员函数
--实例化格子对象
function ItemGrid:Init(father,posX,posY)
    self.obj = ABMgr:LoadRes("ui","itemGrid")
    --设置父对象
    self.obj.transform:SetParent(father,false)
    --继续设置它的位置
    self.obj.transform.localPosition = Vector3(posX,posY,0)
    --找控件
    self.imgIcon = self.obj.transform:Find("btnGrid"):GetComponent(Image)
    self.Text = self.obj.transform:Find("txtNumber"):GetComponent(Text)
    self.btn = self.obj.transform:Find("btnGrid"):GetComponent(Button)
end

--初始化格子信息
--data 是外面传入的道具信息 里面包含了 id 和 num
function ItemGrid:InitData(data)

    local itemData = ItemData[data.id]
    local strs = string.split(itemData.icon,"_")
        
    --加载图集
    local spriteAtlas = ABMgr:LoadRes("ui",strs[1],SpriteAtlas)
    --加载图标
    
    self.imgIcon.sprite = spriteAtlas:GetSprite(strs[2])
    --print(nowItems[i].num)
    --设置它的数量
    self.Text.text = data.num

    self.btn.onClick:AddListener(
        
        function()
            BagPanel.nowId = data.id
            local nameAndTips = ItemData[data.id]
            print(nameAndTips.tips)
            BagPanel.ItemName.text = nameAndTips.name
            BagPanel.ItemDes.text = nameAndTips.tips
        end

    )

end



--加自己的逻辑
function ItemGrid:Destroy()
    GameObject.Destroy(self.obj)
    self.obj = nil
end
