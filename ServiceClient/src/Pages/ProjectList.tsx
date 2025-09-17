import React, { useEffect, useState } from 'react';
import { Card, Button, Modal, Form, Input, Typography, Row, Col, Menu, Dropdown } from 'antd';
import { PlusCircleOutlined, DeleteOutlined, EditOutlined, MoreOutlined, UserAddOutlined } from '@ant-design/icons';
import { getAllProjects, createNewProject, deleteNewProject, updateProject } from '../Services/projectsService';

// Интерфейс описания проекта
interface Project {
  id: string;
  name: string;
  created: string;
  owner: string;
  participants?: string[];
}

interface User{

}

// Основной компонент панели проектов
const Dashboard: React.FunctionComponent = () => {
  // Хуки состояния
  const [projects, setProjects] = useState<Project[]>([]);
  const [newProjectName, setNewProjectName] = useState<string>('');
  const [showModal, setShowModal] = useState(false);
  const [editingProject, setEditingProject] = useState<Project | null>(null);
  const [newOwner, setNewOwner] = useState<string>('');

  // Загрузка всех проектов при монтировании компонента
  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const projectsData = await getAllProjects();
        setProjects(projectsData);
      } catch (error) {
        console.error('Ошибка загрузки проектов:', error);
      }
    };

    fetchProjects();
  }, []);

  // Подтверждение удаления
  const [confirmDeleteVisible, setConfirmDeleteVisible] = useState(false);
  const [projectToDelete, setProjectToDelete] = useState<Project | null>(null);

  // Новый хук для хранения открытого проекта для управления участниками
  const [manageParticipantsOpen, setManageParticipantsOpen] = useState(false);
  const [currentProjectForParticipants, setCurrentProjectForParticipants] = useState<Project | null>(null);

  // Функционал для обработки добавления и удаления участников
  const handleRemoveParticipant = (participant: string) => {
    if (currentProjectForParticipants) {
      // const updatedParticipants = currentProjectForParticipants.participants!.filter((p) => p !== participant);
      // Отправьте запрос на сервер для обновления участников проекта
      // Примечание: Необходимо реализовать метод updatedParticipants
      alert("Участник удалён");
    }
  };

  // Генерируем список участников с кнопками удаления
  const renderParticipantsList = () => {
    if (currentProjectForParticipants && currentProjectForParticipants.participants) {
      return currentProjectForParticipants.participants.map((participant, index) => (
        <div key={index} style={{ display: 'flex', alignItems: 'center', marginBottom: '8px' }}>
          <span style={{ flexGrow: 1 }}>{participant}</span>
          <Button type="link" danger onClick={() => handleRemoveParticipant(participant)}>
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
  const manageParticipants = (project: Project) => {
    setCurrentProjectForParticipants(project);
    setManageParticipantsOpen(true);
  };

  // Окно подтверждения удаления проекта
  const showDeleteConfirmation = (project: Project) => {
    setProjectToDelete(project);
    setConfirmDeleteVisible(true);
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
            <Button type="primary" /*block onClick={() => addNewParticipant()}*/>
              <UserAddOutlined /> Добавить участника
            </Button>
            {renderParticipantsList()}
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