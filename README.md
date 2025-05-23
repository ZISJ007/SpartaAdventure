## 🎮조작 키
wasd : 이동
space : 점프
F : 상호작용
마우스 : 시점 조절
---

## Ray, Raycast
- **Ray**
  - GameObject를 사용하여 Ray의 위치를 설정할 수 있음
  - Raycast를 사용하여 범위내에 아이템 layer와 Item 스크립트에 저장된 ItemData의 정보를 감지하여 아이템 이름, 효과 등 정보를 표시
  - ![image](https://github.com/user-attachments/assets/4d33a9f8-063c-4cf1-b5ec-c1ea8c40adaa)
---

## Item, Inventory
- **Item**
  - itemAbilityType은   SpeedUp, JumpUp로 이동속도 추가와 점프력 추가 아이템을 추가
  -  plusValue을 아이템 효과 공용 수치로 사용
  -  Item은 Player tag와 충돌하면 삭제되며 인벤토리에 저장된다.
  -  Item 사용 시 itemAbilityType에 따라 능력치를 올리고 설정한 시간이 지나면 원래 능력치로 돌아온다.
  -  ![image](https://github.com/user-attachments/assets/8b5c260c-8637-4511-b32f-042f90b92dd0)

- **Inventory**
  - Inventory는 3개가 존재하여 순서대로 1번 키, 2번 키, 3번 키를 사용하여 아이템 사용 가능
  - ![image](https://github.com/user-attachments/assets/2a9bc1a3-1ff1-4dbf-b41d-93b4ccc97ce5)
---

## JumpPad
  - JumpPad 오브젝트를 밟으면 Impulse를 사용하여 캐릭터의 Y 값이 증가한다.
  - JumpPad 중복 사용을 막기위해 코루틴을 사용하여 설정한 시간 동안 JumpPad 콜라이더 비활성화
  - ![image](https://github.com/user-attachments/assets/732df85e-22a3-4208-a0e3-c7d42260ceb3)
---

## Obstacle
  - Player tag와 충돌하면 playerCondition.Damaged()를 호출하여 데미지 처리
---

## PlayerAnimations
  - Idle, Run, Jump, Falling, Die로 구성
  - Falling은 !IsGrounded일 때 실행하여 떨어지는 애니메이션 구현
  - Jump는 IsGrounded일 때만 실행하여 중복 점프 방지
  - Die는 Hp가 0이하가 되면 실행
  - Die는 Hp가 0이하가 되면 실행하여 this.enabled = false로 스크립트 비활성화로 정지
  - ![image](https://github.com/user-attachments/assets/f8a765b9-acdf-451a-b617-44f3e22a3989)
---

## PlayerController
  - Die가 되면 this.enabled = false로 스크립트 비활성화로 정지
  - inputAction을 사용한 조작 구현
---

## PlayerUI
  - Obstacle과 충돌하면 Hp가 감소
  - ![image](https://github.com/user-attachments/assets/b56b7a0f-c084-4709-b0bf-90aae2e2b1d7)
  - ![image](https://github.com/user-attachments/assets/b1a66c9f-fbfc-44bd-9892-1638db6ac52d)

