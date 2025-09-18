import React, { useEffect, useState } from 'react';
import { Card, Button, Modal, Form, Input, Typography, Row, Col, Menu, Dropdown } from 'antd';
import { PlusCircleOutlined, DeleteOutlined, EditOutlined, MoreOutlined, UserAddOutlined } from '@ant-design/icons';
import { getAllProjects, createNewProject, deleteNewProject, updateProject, addUserToProject, deleteUserFromProject, getUsers } from '../Services/projectsService';

// Интерфейс описания проекта
interface Project {
  id: string;
  name: string;
  created: string;
  owner: string;
  users?: User[];
}

interface User {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
}

// Основной компонент панели проектов
const Dashboard: React.FunctionComponent = () => {
  // Хуки состояния
  const [projects, setProjects] = useState<Project[]>([]);
  const [newProjectName, setNewProjectName] = useState<string>('');
  const [showModal, setShowModal] = useState(false);
  const [editingProject, setEditingProject] = useState<Project | null>(null);
  const [newOwner, setNewOwner] = useState<string>('');

  // Новое состояние для отслеживания всех пользователей
  const [allUsers, setAllUsers] = useState<User[]>([]);

  // Состояния для отображения модальных окон
  const [selectUserModalOpen, setSelectUserModalOpen] = useState(false);
  const [manageParticipantsOpen, setManageParticipantsOpen] = useState(false);

  // Подтверждение удаления
  const [confirmDeleteVisible, setConfirmDeleteVisible] = useState(false);
  const [projectToDelete, setProjectToDelete] = useState<Project | null>(null);

  // Текущий проект для управления участниками
  const [currentProjectForParticipants, setCurrentProjectForParticipants] = useState<Project | null>(null);

  // Загрузка всех проектов и пользователей при монтировании компонента
  useEffect(() => {
    const fetchProjectsAndUsers = async () => {
      try {
        const projectsData = await getAllProjects();
        const usersData = await getUsers(); // Получаем всех пользователей
        setProjects(projectsData);
        setAllUsers(usersData); // Устанавливаем пользователей в состоянии
      } catch (error) {
        console.error('Ошибка загрузки данных:', error);
      }
    };

    fetchProjectsAndUsers();
  }, []);

  // Обработчик открытия модального окна выбора пользователей
  const openSelectUserModal = () => {
    setSelectUserModalOpen(true);
  };

  // Фильтрация пользователей, которые ещё не состоят в проекте
  const filteredUsers = allUsers.filter(user => {
    return !currentProjectForParticipants?.users.some(p => p.id === user.id);
  });

  // Функция закрытия модального окна
  const closeSelectUserModal = () => {
    setSelectUserModalOpen(false);
  };

  // Функционал для обработки добавления и удаления участников
  const handleRemoveParticipant = async (participant: string) => {
    if (currentProjectForParticipants) {
      deleteUserFromProject(currentProjectForParticipants.id, participant);
      setManageParticipantsOpen(false);
      const updatedProjectsList = await getAllProjects();
      setProjects(updatedProjectsList);
    }
  };

  // Генерируем список участников с кнопками удаления
  const renderParticipantsList = () => {
    if (currentProjectForParticipants && currentProjectForParticipants.users) {
      return currentProjectForParticipants.users.map((participant, index) => (
        <div key={index} style={{ display: 'flex', alignItems: 'center', marginBottom: '8px' }}>
          <span style={{ flexGrow: 3 }}>{participant.firstName} {participant.lastName}</span>
          <span style={{ flexGrow: 1 }}>{participant.userName}</span>
          <Button type="link" danger onClick={() => handleRemoveParticipant(participant.id)}>
            <DeleteOutlined />
          </Button>
        </div>
      ));
    }
    return [];
  };

  // Обработчик создания нового проекта
  const handleCreateProject = async () => {
    try {
      await createNewProject(newProjectName);
      const updatedProjectsList = await getAllProjects();
      setProjects(updatedProjectsList);
      setShowModal(false); // Закрываем модальное окно
      setNewProjectName(''); // Очищаем форму ввода названия
    } catch (error) {
      console.error('Ошибка создания проекта:', error);
    }
  };

  // Обработка подтверждения удаления проекта
  const confirmDeleteProject = async () => {
    if (!projectToDelete) return;

    try {
      await deleteNewProject(projectToDelete.id);
      const updatedProjectsList = await getAllProjects();
      setProjects(updatedProjectsList);
      setProjectToDelete(null);
      setConfirmDeleteVisible(false);
    } catch (error) {
      console.error('Ошибка удаления проекта:', error);
    }
  };

  // Меню для трех точек
  const menuOptions = (record: Project) => [
    <Menu.Item key="edit" onClick={() => openEditModal(record)}>
      <span><EditOutlined style={{ fontSize: '16px' }} /> Редактировать</span>
    </Menu.Item>,
    <Menu.Divider />,
    <Menu.Item key="change-participants" onClick={() => manageParticipants(record)}>
      <span><UserAddOutlined style={{ fontSize: '16px' }} /> Изменить состав участников</span>
    </Menu.Item>,
    <Menu.Divider />,
    <Menu.Item key="delete" danger onClick={() => showDeleteConfirmation(record)}>
      <span><DeleteOutlined style={{ fontSize: '16px' }} /> Удалить</span>
    </Menu.Item>,
  ];

  // Редактирование проекта
  const handleEditProject = async () => {
    if (!editingProject) return;

    try {
      await updateProject(editingProject.id, editingProject.name, newOwner);
      const updatedProjectsList = await getAllProjects();
      setProjects(updatedProjectsList);
      setEditingProject(null); // Закрываем модальное окно редактирования
      setNewOwner(''); // Очищаем форму имени владельца
    } catch (error) {
      console.error('Ошибка обновления проекта:', error);
    }
  };

  // Открытие окна редактирования проекта
  const openEditModal = (project: Project) => {
    setEditingProject(project);
    setNewOwner(project.owner);
  };

  // Управление составом участников
  const manageParticipants = async (project: Project) => {
    const updatedProjectsList = await getAllProjects();
    setProjects(updatedProjectsList);
    setCurrentProjectForParticipants(project);
    setManageParticipantsOpen(true);
  };

  // Окно подтверждения удаления проекта
  const showDeleteConfirmation = (project: Project) => {
    setProjectToDelete(project);
    setConfirmDeleteVisible(true);
  };

  // Обновляем проекты и закрываем модал после добавления пользователя
  const addUserToProjectHandler = async (projectId: string, userId: string) => {
    try {
      await addUserToProject(projectId, userId);
      const updatedProjectsList = await getAllProjects();
      setProjects(updatedProjectsList);
      closeSelectUserModal(); // Закрываем модальное окно
      setManageParticipantsOpen(false);
    } catch (error) {
      console.error('Ошибка добавления пользователя:', error);
    }
  };

  return (
    <>
      {/* Шапка */}
      <Row justify="start" align="middle" className="dashboard-header">
        <Col span={8}>
          <Typography.Title level={2}>Панель проектов</Typography.Title>
        </Col>
      </Row>

      {/* Модальное окно создания проекта */}
      {showModal && (
        <Modal
          title="Создание нового проекта"
          visible={showModal}
          onOk={handleCreateProject}
          onCancel={() => setShowModal(false)}
          okText="Создать"
          cancelText="Отмена"
        >
          <Form layout="vertical">
            <Form.Item label="Название проекта:">
              <Input
                value={newProjectName}
                onChange={(e) => setNewProjectName(e.target.value)}
                placeholder="Введите название проекта"
              />
            </Form.Item>
          </Form>
        </Modal>
      )}

      {/* Модальное окно редактирования проекта */}
      {editingProject && (
        <Modal
          title={`Редактирование проекта ${editingProject?.name}`}
          visible={!!editingProject}
          onOk={handleEditProject}
          onCancel={() => setEditingProject(null)}
          okText="Обновить"
          cancelText="Отменить"
        >
          <Form layout="vertical">
            <Form.Item label="Название проекта:">
              <Input
                value={editingProject.name}
                onChange={(e) =>
                  setEditingProject({
                    ...editingProject,
                    name: e.target.value,
                  })
                }
              />
            </Form.Item>
            <Form.Item label="Имя владельца:">
              <Input
                value={newOwner}
                onChange={(e) => setNewOwner(e.target.value)}
                placeholder="Введите имя владельца"
              />
            </Form.Item>
          </Form>
        </Modal>
      )}

      {/* Окно подтверждения удаления проекта */}
      {confirmDeleteVisible && projectToDelete && (
        <Modal
          title={`Удаление проекта "${projectToDelete.name}"`}
          visible={confirmDeleteVisible}
          onOk={confirmDeleteProject}
          onCancel={() => setConfirmDeleteVisible(false)}
          okText="Да"
          cancelText="Отмена"
        >
          <p>Вы действительно хотите удалить этот проект?</p>
        </Modal>
      )}

      {/* Модальное окно управления участниками проекта */}
      {manageParticipantsOpen && currentProjectForParticipants && (
        <Modal
          title={`Управление участниками проекта "${currentProjectForParticipants.name}"`}
          visible={manageParticipantsOpen}
          onCancel={() => setManageParticipantsOpen(false)}
          footer={null}
        >
          <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
            <Button type="primary" onClick={openSelectUserModal}>
              <UserAddOutlined /> Добавить участника
            </Button>
            {renderParticipantsList()}
          </div>
        </Modal>
      )}

      {/* Модальное окно выбора пользователей для добавления в проект */}
      {selectUserModalOpen && filteredUsers && (
        <Modal
          title="Добавление участников в проект"
          visible={selectUserModalOpen}
          onCancel={closeSelectUserModal}
          footer={null}
        >
          <div style={{ maxHeight: '400px', overflowY: 'scroll' }}>
            {filteredUsers.map((user) => (
              <div key={user.id} style={{ display: 'flex', alignItems: 'center', marginBottom: '8px' }}>
                <span style={{ flexGrow: 1 }}>{user.userName}: {user.email}</span>
                <Button type="link" onClick={() => addUserToProjectHandler(currentProjectForParticipants!.id, user.id)}>
                  <PlusCircleOutlined />
                </Button>
              </div>
            ))}
          </div>
        </Modal>
      )}

      {/* Карточки отображения проектов */}
      <Row gutter={[16, 16]} className="projects-list">
        <Col xs={24} sm={12} md={8} lg={6} xl={4}>
          <Card
            hoverable
            bordered
            bodyStyle={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}
            style={{ backgroundColor: '#fff', position: 'relative', overflowY: 'auto', borderColor: '#67c23a', color: '#67c23a', cursor: 'pointer', height: '100%', width: '100%' }}
            onClick={() => setShowModal(true)}
          >
            <PlusCircleOutlined style={{ fontSize: '24px' }} />
          </Card>
        </Col>
        {projects.length > 0 ? (
          projects.map((project) => (
            <Col xs={24} sm={12} md={8} lg={6} xl={4} key={project.id}>
              <Card hoverable bordered style={{ height: '100%', position: 'relative' }}>
                <Dropdown overlay={() => (<Menu>{menuOptions(project)}</Menu>)}>
                  <MoreOutlined style={{ float: 'right', marginTop: '-8px', marginRight: '-8px', cursor: 'pointer', fontSize: '20px', fontWeight:'bold' }} />
                </Dropdown>
                <div style={{ paddingBottom: '56px', overflowY: 'auto' }}>
                  <Typography.Title level={3}>{project.name}</Typography.Title>
                  <Typography.Paragraph>Владелец: {project.owner}</Typography.Paragraph>
                </div>
                {/* Зафиксированный футер с датой создания */}
                <div style={{ position: 'absolute', bottom: 0, right: 0, color: '#999', fontSize: '12px', padding: '10px' }}>
                  {new Date(project.created).toLocaleDateString()}
                </div>
              </Card>
            </Col>
          ))
        ) : (
          <Col span={24}>
            <Typography.Text type="secondary">Нет проектов.</Typography.Text>
          </Col>
        )}
      </Row>
    </>
  );
};

export default Dashboard;