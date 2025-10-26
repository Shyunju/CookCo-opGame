개발 기간 2025/05/15 ~
<br>
5/15 ~ 20 캐릭터 기본 키보드 조작 <br>
5/20 ~ 6/3 음식 아이템, (박스,컷팅)테이블 제작 <br>
6/3 ~ 5 플레이어 애니메이션(state Machine) <br>
6/5 ~ 12조리도구 아이템, (구이, 끓이기)테이블 제작 <br>
6/12 ~ 19 접시에 요리 담기, 도구에 담긴 재료 UI <br>
6/19 ~ 26 설거지 테이블, 버리기 테이블<br>
6/27 ~ 7/1 제출 테이블<br>
7/2 ~ 3 아이템 json파일 화 후 연동 <br>
7/4  ~ 8 재료 UI표시, 재료 food prefabs 제작, recipe json파일 화 후 연동 <br>  
7/ 9 ~ 10 재료 조힙별 food mesh change system <br>
7/11 ~ 15 도구 burn system 제작<br>
7/16 ~ 17 fix bug <br>
7/18 ~ 23 make image, sound resources  & order UI <br>
7/23 ~ 24 map, level design & add sound system <br> 
7/25 ~  29 make mouse , add UI <br>
7/30 ~ 8/7 쥐 아이템 픽업 엔드 뺏기 <br>
8/8 ~ 26로비씬, 메인씬 데코, 로비 샵 UI & 구매 시스템 구성 <br>
9/8 ~ 9/23 how to ui, save <br>
25 ~ 30exit game, setting ui <br>
sound mixer<br>
object pool?<br>
json암호화
![header](https://capsule-render.vercel.app/api?type=waving&color=gradient&customColorList=19&height=300&section=header&text=OrderUp&fontSize=90&fontColor=fff76b)
## 🎮 게임 소개


- **게임 명: `CookCo-opGame`**
- **장르**: 코옵, 시뮬레이션
- **개발 도구**: mac, Unity6, uGUI, Visual Studio Code, git hub, Perplexity(AI), Gemini CLI(AI)
- **개발 기간**: 2025/05/15 ~ 2025/09/ (이후 버그수정
- **출시 플렛폼**: PC
- **제작**: 기획 - 신현주, programing - 신현주, UI design - 신현주
<br><br>
- **게임 개요:**
    - one PC two Player 방식의 2인 협동 게임입니다.
    - 정해진 시간동안 요리를 하면서 골드를 벌 수 있습니다.
    - 획득한 골드로 재료와 레시피를 구매하여 한 번의 플레이 동안 더 많은 골드를 벌 수 있습니다.
    - 주문으로 들어오지 않은 요리를 만들어내면 손해비용이 발생합니다.
    - 주방안에 음식이 그냥 놓여있다면 쥐가 나타나 음식을 훔쳐갑니다.
    - 쥐가 집으로 돌아갈때까지 음식을 되찾지 못하면 라이프가 감소합니다.
    - 3개의 라이프가 모두 감소되면 그 차례의 요리시간이 남아있어도 종료됩니다.
    - 목표 금액을 획득하는 것이 최종 목표입니다.

 - **조작법:**    
    | 키 | 수행 기능 |
    | --- | --- |
    | 마우스 | UI 조작 |
    | W,A,S,D / 방향키 | 캐릭터 이동 |
    | V / Left Alt | 잡기, 놓기 |
    | B / Left Ctrl | 상호작용 |
    | N / Left Shift | 대쉬, 던지기 |
